namespace Galaga.States;

using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using Galaga.MovementStrategy;
using Galaga.Squadron;
using Galaga.HitStrategy;
using DIKUArcade.Audio;

public class GameRunning : IGameState {
    private StateMachine stateMachine;
    private Player player;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private GameEventBus gameEventBus;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;
    private Text scoreText;
    private Text winText = new Text("", new Vector2(0.25f, 0.5f));
    private Image backgroundImage;
    private SoundEffectAudio soundEffect;
    
    public static int Score { get; set; }

    public GameRunning(StateMachine stateMachine) {
        backgroundImage = new Image("Galaga.Assets.Images.SpaceBackground.png");
        Score = 0;
        Random random = new Random();
        scoreText = new Text($"Score: {Score}", new Vector2(0.0f, 0.025f), 0.5f); 

        this.stateMachine = stateMachine;
        // Player   
        player = new Player(
            new DynamicShape(new Vector2(0.45f, 0.10f),
                             new Vector2(0.10f, 0.10f)),
            new Image("Galaga.Assets.Images.Player.png"));

        // Strategies
        ISquadron[] squadStrategies = new ISquadron[] {
            new ZigZagSquadron(),
            new BlockSquadron(),
            new HalfCircleSquadron()
        };

        // Enemies
        List<Image> enemiesImg = ImageStride.CreateStrides(4, "Galaga.Assets.Images.BlueMonster.png");

        enemies = squadStrategies[random.Next(3)].CreateEnemies(
            enemiesImg,
            () => random.Next(2) switch {
                0 => new Down(),
                1 => new ZigZagDown(),
                _ => throw new Exception("Issue with random generator!")
            },
            () => random.Next(3) switch {
                0 => new Enrage(),
                1 => new Teleport(),
                2 => new Speed(),
                _ => throw new Exception("Issue with random generator!")
            }
        );

        // Shots
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image("Galaga.Assets.Images.BulletRed2.png");

        // Game event bus 
        gameEventBus = GalagaBus.Instance;
        gameEventBus.Subscribe<AddExplosionEvent>(AddExplosion);

        // Explosions
        enemyExplosions = new AnimationContainer(8);
        explosionStrides = ImageStride.CreateStrides(
            8, "Galaga.Assets.Images.Explosion.png"
        );

        soundEffect = new SoundEffectAudio("Galaga.Assets.coin.wav");
    }

    ~GameRunning() {
        gameEventBus.Unsubscribe<AddExplosionEvent>(AddExplosion);
    }

    private void IterateShots() {
        playerShots.Iterate(shot => {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    DynamicShape shotDynamic = shot.Shape.AsDynamicShape();
                    CollisionData occuredCollision = CollisionDetection.Aabb(shotDynamic,
                                                                            enemy.Shape);
                    if (occuredCollision.Collision) {
                        gameEventBus.RegisterEvent(
                            new AddExplosionEvent(enemy.Shape.Position, enemy.Shape.Extent)
                        );
                        shot.DeleteEntity();
                        enemy.Hit(enemy);
                    }
                });
            }
        });
    }
    
    public void AddExplosion(AddExplosionEvent addExplosionEvent) {
        Shape explosionShape = new StationaryShape(addExplosionEvent.Position,
                                                   addExplosionEvent.Extent);
        enemyExplosions.AddAnimation(explosionShape,
                                     EXPLOSION_LENGTH_MS,
                                     new ImageStride(EXPLOSION_LENGTH_MS / explosionStrides.Count,
                                                     explosionStrides)
                                     );
    }

    public void Update() {
        player.Move();
        IterateShots();
        gameEventBus.ProcessEvents();
        scoreText.SetText($"Score: {Score}");
        if (Score == 8) {
            winText.SetText("You Win!");
        }

        foreach (Enemy e in enemies) {
            e.Move();
        }
    }

    public void Render(WindowContext context) {
        backgroundImage.Render(context,new StationaryShape(
                    new Vector2(0.0f, 0.0f),
                    new Vector2(1.0f, 1.0f))
                );
        player.RenderEntity(context);
        enemies.RenderEntities(context);
        playerShots.RenderEntities(context);
        enemyExplosions.RenderAnimations(context);
        scoreText.Render(context);
        winText.Render(context);
    }  

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        player.Keyhandler(action, key);
        if (action == KeyboardAction.KeyRelease) {
            if (key == KeyboardKey.Space) {
                Vector2 playerPos = player.GetPosition;
                playerPos.X += 0.046f;
                PlayerShot shot = new PlayerShot(playerPos, playerShotImage);
                playerShots.AddEntity(shot);
                soundEffect.Play();
            }
        } else if (action == KeyboardAction.KeyPress) {
            if (key == KeyboardKey.Escape) {
                stateMachine.ActiveState = new GamePaused(stateMachine);
            } else if (key == KeyboardKey.F) {
                Vector2 playerPos = player.GetPosition;
                playerPos.X += 0.046f;
                PlayerShot shot = new PlayerShot(playerPos, playerShotImage);
                playerShots.AddEntity(shot);
                soundEffect.PlaySoundMulti();
            }
        }
    }
}

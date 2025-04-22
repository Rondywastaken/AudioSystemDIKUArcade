namespace Galaga.States;

using System.Numerics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

public class GamePaused : IGameState {

    private StateMachine stateMachine;
    private Text[] texts;
    private Text pauseText;
    private int active = 0;
    private Image backgroundImage;

    public GamePaused(StateMachine stateMachine) {
        this.stateMachine = stateMachine;
        backgroundImage = new Image("Galaga.Assets.Images.SpaceBackground.png");
        pauseText = new Text("Game Paused!", new Vector2(0.15f, 0.7f), 0.7f);
        texts = new Text[] { 
            new Text("- Continue", new Vector2(0.15f, 0.6f), 0.5f),
            new Text("- Main Menu", new Vector2(0.15f, 0.5f), 0.5f) 
        };
        texts[0].SetColor(50, 155, 7);
    }

    public void Update() {}

    public void Render(WindowContext context) {
        backgroundImage.Render(context, new StationaryShape(
                    new Vector2(0.0f, 0.0f),
                    new Vector2(1.0f, 1.0f))
                );
        pauseText.Render(context);

        foreach (Text t in texts) {
            t.Render(context);
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            switch (key) {
                case KeyboardKey.Down:
                    if (active == 0) {
                        texts[1].SetColor(50, 155, 7);
                        texts[0].SetColor(255, 255, 255);
                        active++;
                        break;
                    } else {
                        break;
                    }
                case KeyboardKey.Up:
                    if (active == 1) {
                        texts[0].SetColor(50, 155, 7);
                        texts[1].SetColor(255, 255, 255);
                        active--;
                        break;
                    } else {
                        break;
                    }
                case KeyboardKey.Enter:
                    if (active == 0) {
                        stateMachine.ActiveState = stateMachine.PreviousState;
                        break;
                    } else if (active == 1) {
                        stateMachine.ActiveState = new MainMenu(stateMachine);
                        break;
                    }
                    break;
                }
            }
        }
    }


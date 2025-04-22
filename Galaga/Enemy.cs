namespace Galaga;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga.HitStrategy;
using Galaga.MovementStrategy;
using Galaga.States;

public class Enemy : Entity {

    private IHitStrategy hitStrategy;
    private IMovementStrategy movement;

    private int hitPoints;
    public int HitPoints {
        get {
            return hitPoints;
        }
        set {
            hitPoints = value;
        }
    }
    
    public Enemy(DynamicShape shape, IBaseImage image, IMovementStrategy movement, IHitStrategy hit) : base(shape, image) {
        hitPoints = 40;
        hitStrategy = hit;
        this.movement = movement;
    }

    public void Hit(Enemy enemy) {
        enemy.HitPoints -= 10;
        hitStrategy.Hit(enemy);
        if (enemy.HitPoints <= 0) {
            Dead(enemy);
        }
    }
    public void Dead(Enemy enemy) {
        enemy.DeleteEntity();
        GameRunning.Score++;
    }

    public void Move() {
        movement.Move(this);
    }

    public void Scale(float factor) {
        movement.Scale(factor);
    }
}

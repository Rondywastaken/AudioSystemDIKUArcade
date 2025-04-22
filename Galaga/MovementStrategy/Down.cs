namespace Galaga.MovementStrategy;

using System;

public class Down : IMovementStrategy {
    private bool changeSpeed = true;
    private float speed = 0.0008f;

    public void Scale(float factor) {
        speed *= factor;
        // Allow for scaling speed
        changeSpeed = true;
    }

    public void Move(Enemy enemy) {
        // Set enemy's velocity
        if (changeSpeed) {
            enemy.Shape.AsDynamicShape().Velocity.Y = -speed;
            changeSpeed = false;
        }
        enemy.Shape.Move();
    }
}

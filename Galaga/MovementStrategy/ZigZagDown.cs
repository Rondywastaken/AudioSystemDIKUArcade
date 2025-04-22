namespace Galaga.MovementStrategy;

using System;

public class ZigZagDown : IMovementStrategy {
    float s = 0.0003f;
    float p = 0.045f;
    float a = 0.05f;
    float x_0;
    float y_0;
    float x;
    float y;
    bool isFirstIteration = true;

    public void Scale(float factor) {
        s *= factor;
    }

    public void Move(Enemy enemy) {
        // Store start positions
        if (isFirstIteration) {
            x_0 = enemy.Shape.Position.X;
            y_0 = enemy.Shape.Position.Y;
            isFirstIteration = false;
        }

        // Current positions
        x = enemy.Shape.Position.X;
        y = enemy.Shape.Position.Y;

        // Next position
        float y_i = y + s;
        float x_i = x_0 + a * MathF.Sin( (2 * MathF.PI * (y_0 - y_i)) / p );

        // Get difference and update velocity
        enemy.Shape.AsDynamicShape().Velocity.X = (x_i - x);
        enemy.Shape.AsDynamicShape().Velocity.Y = -(y_i - y);
        enemy.Shape.Move();
    }
}

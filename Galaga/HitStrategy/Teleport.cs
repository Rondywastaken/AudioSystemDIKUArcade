namespace Galaga.HitStrategy;

using System;
using System.Numerics;


public class Teleport : IHitStrategy {
    Random random = new Random();
    public void Hit(Enemy enemy) {
        double randomY = random.NextDouble() * (0.9 - 0.33) + 0.33;
        double randomX = random.NextDouble() * (0.9 - 0.1) + 0.1;

        enemy.Shape.Position = new Vector2((float) randomX, (float) randomY);
    }

}
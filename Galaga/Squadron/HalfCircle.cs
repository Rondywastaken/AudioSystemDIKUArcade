namespace Galaga.Squadron;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System;
using System.Numerics;
using Galaga.MovementStrategy;
using Galaga.HitStrategy;

public class HalfCircleSquadron : ISquadron {
    public EntityContainer<Enemy> CreateEnemies(
        List<Image> enemyStrides, 
        Func<IMovementStrategy> movement,  
        Func<IHitStrategy> hit
    ) {
        var enemies = new EntityContainer<Enemy>();
        const int numEnemies = 8;
        for (int i = 0; i < numEnemies; i++) {
            float x = 0.1f + (i * 0.1f);
            float y = 0.9f - (float)Math.Pow(i - 3.5, 2) * 0.015f;

            var enemy = new Enemy(
                new DynamicShape(new Vector2(x, y), new Vector2(0.1f, 0.1f)), 
                new ImageStride(80, enemyStrides),
                movement(),
                hit()
            );
            enemies.AddEntity(enemy);
        }
        return enemies;
    }
}

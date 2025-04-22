namespace Galaga.Squadron;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga.MovementStrategy;
using Galaga.HitStrategy;

public interface ISquadron {
    EntityContainer<Enemy> CreateEnemies(
        List<Image> enemyStrides,
        Func<IMovementStrategy> movement,
        Func<IHitStrategy> hit
    );
}

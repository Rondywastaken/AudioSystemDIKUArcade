
namespace Galaga.HitStrategy;

using System;
public class Speed : IHitStrategy {

    public void Hit(Enemy enemy) {
        enemy.Scale(2.0f);
    }

}
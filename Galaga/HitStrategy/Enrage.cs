namespace Galaga.HitStrategy;

using System;
using System.Collections.Generic;
using DIKUArcade.Graphics;


public class Enrage : IHitStrategy {
    List<Image> images = ImageStride.CreateStrides(2, "Galaga.Assets.Images.RedMonster.png");
    public void Hit(Enemy enemy) {
        if (enemy.HitPoints <= 20) {
            enemy.Image = new ImageStride(80, images);
            enemy.Scale(4.0f);
        }
    }
}

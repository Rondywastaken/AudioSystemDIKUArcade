namespace Galaga;

using System;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;


public class Player : Entity {

    private float moveLeft;
    private float moveRight;
    const float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        moveLeft = 0.00f;
        moveRight = 0.00f;
    }

    public Vector2 GetPosition {
        get => Shape.Position;
    }

    public void Move() {
        DynamicShape player = Shape.AsDynamicShape();
        float roundedPos = MathF.Round(player.Position.X, 3);
        float preMoveLeft = roundedPos + player.Velocity.X;
        float preMoveRight = roundedPos + player.Velocity.X;

        if (!(preMoveLeft < 0.0f) && !(preMoveRight > 1.0f - 0.1f)) {
            player.Move();
        }
    }

    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft = -MOVEMENT_SPEED;
        } else {
            moveLeft = 0.00f;
        }
        UpdateVelocity();
    }

    
    public void SetMoveRight(bool val) {
        if (val) {
            moveRight = MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        }
        UpdateVelocity();
    }


    private void UpdateVelocity() {
        DynamicShape player = Shape.AsDynamicShape();
        player.Velocity.X = moveLeft + moveRight;
    }

    public void Keyhandler(KeyboardAction action, KeyboardKey key){
        if (action == KeyboardAction.KeyRelease){
            switch (key){
                case KeyboardKey.Right:
                    SetMoveRight(false);
                    break;
                    
                case KeyboardKey.Left:
                    SetMoveLeft(false);
                    break;
            }
        } else {
            switch (key){
                case KeyboardKey.Right:
                    SetMoveRight(true);
                    break;
                
                case KeyboardKey.Left:
                    SetMoveLeft(true);
                    break;
            }
        }
    }
}

namespace Galaga;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Galaga.States;

public class Game : DIKUGame {

    public static bool IsRunning { get; set; }

    private StateMachine stateMachine;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
        IsRunning = true;
    }

    public override void Render(WindowContext context) {
        stateMachine.ActiveState.Render(context);
    }

    public override void Update() {
        stateMachine.ActiveState.Update();
        if (!IsRunning) {
            window.CloseWindow();
        }
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }

}

namespace Galaga.States;

using DIKUArcade.Input;
using DIKUArcade.GUI;

public interface IGameState {
    void Update();
    void Render(WindowContext context);
    void HandleKeyEvent(KeyboardAction action, KeyboardKey key);
}

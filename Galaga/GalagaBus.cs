namespace Galaga;

using DIKUArcade.Events;

public sealed class GalagaBus {
    private static GameEventBus? instance = null;

    private GalagaBus() { }

    public static GameEventBus Instance {
        get {
            if (instance == null) {
                instance = new GameEventBus();
            }
            return instance;
        }
    }
}

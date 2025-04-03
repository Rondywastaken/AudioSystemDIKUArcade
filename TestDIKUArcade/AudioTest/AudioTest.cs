namespace TestDIKUArcade.AudioTest;

using System;
using DIKUArcade.GUI;

public class AudioTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "AudioTest",
            Width = 600
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Do you hear the audio?";
        Console.WriteLine(help);
    }
}


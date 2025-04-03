namespace TestDIKUArcade.AudioTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Audio;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using System;
using System.Numerics;

public class Game : DIKUGame {
    MusicAudio music;
    SoundEffectAudio sound;
    bool paused = false;
    Text[] texts = new Text[] {
        new Text("Click 'I' to play music", new Vector2(0.05f, 0.9f), 0.4f),
        new Text("Click 'O' to pause/resume music", new Vector2(0.05f, 0.85f), 0.4f),
        new Text("Click 'P' to stop music", new Vector2(0.05f, 0.8f), 0.4f),
        new Text("Click 'Up'/'Down' to increase or decrease volume", new Vector2(0.05f, 0.75f), 0.4f),
        new Text("Click 'W'/'S' to increase or decrease pitch", new Vector2(0.05f, 0.7f), 0.4f),
        new Text("Hold 'Space' to play multisound", new Vector2(0.05f, 0.65f), 0.4f),
    };

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        music = new MusicAudio("TestDIKUArcade.Assets.music.ogg");
        sound = new SoundEffectAudio("TestDIKUArcade.Assets.coin.wav");
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            switch (key) {
                case KeyboardKey.I:
                    music.Play();
                    Console.WriteLine("played audio");
                    break;
                case KeyboardKey.O:
                    if (!paused && music.IsPlaying) {
                        music.Pause();
                        Console.WriteLine("Paused audio");
                        paused = true;
                    } else if (paused && !music.IsPlaying) {
                        music.Resume();
                        Console.WriteLine("Resumed audio");
                        paused = false;
                    }
                    break;
                case KeyboardKey.P: 
                    music.Stop();
                    Console.WriteLine("Stopped Audio");
                    break;
                case KeyboardKey.Up: 
                    music.Volume += 0.1f;
                    Console.WriteLine("Increased volume");
                    break;
                case KeyboardKey.Down: 
                    music.Volume -= 0.1f;
                    Console.WriteLine("Decreased volume");
                    break;
                case KeyboardKey.W: 
                    music.Pitch += 0.01f;
                    Console.WriteLine("Increased pitch");
                    break;
                case KeyboardKey.S: 
                    music.Pitch -= 0.01f;
                    Console.WriteLine("Decreased pitch");
                    break;
                case KeyboardKey.Space:
                    sound.PlaySoundMulti();
                    break;
            }
        }
    }

    public override void Render(WindowContext context) {
        foreach (Text text in texts) {
            text.Render(context);
        }
    }

    public override void Update() {
        music.Update();
    }

}

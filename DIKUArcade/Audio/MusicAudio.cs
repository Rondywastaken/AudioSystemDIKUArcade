namespace DIKUArcade.Audio;

using Raylib_cs;
using System.Reflection;
using System;

public class MusicAudio : Audio {

    private Music music;
    public override bool IsPlaying { get => Raylib.IsMusicStreamPlaying(music); }
    public override float Volume { 
        get => volume;
        set {
            volume = Math.Clamp(value, 0.0f, 1.0f);
            Raylib.SetMusicVolume(music, volume);
        }
    }
    public override float Pitch {
        get => pitch;
        set {
            pitch = Math.Clamp(value, 0.5f, 1.5f);
            Raylib.SetMusicPitch(music, pitch);
            Console.WriteLine(pitch);
        }
    }

    public MusicAudio(string manifestResourceName) 
        : base(manifestResourceName, Assembly.GetCallingAssembly()) {
        music = Raylib.LoadMusicStream(Path);
        Volume = volume;
    }   

    public MusicAudio(string manifestResourceName, bool loop) 
        : base(manifestResourceName, Assembly.GetCallingAssembly(), loop) {
        music = Raylib.LoadMusicStream(Path);
        Volume = volume;
    }   

    public override void Play() {
        Raylib.PlayMusicStream(music);
    }

    public override void Stop() {
        Raylib.StopMusicStream(music);
    }

    public override void Pause() {
        if (IsPlaying) {
            Raylib.PauseMusicStream(music);
        }
    }

    public override void Resume() {
        if (!IsPlaying) {
            Raylib.ResumeMusicStream(music);
        }
    }

    public void Update() {
        Raylib.UpdateMusicStream(music);
        float timePlayed = MathF.Round(Raylib.GetMusicTimePlayed(music));
        float timeLength = MathF.Round(Raylib.GetMusicTimeLength(music));
        //Console.WriteLine($"{timePlayed} / {timeLength}");
        // By default music gets looped
        if (!Loop) {
            if (timePlayed == timeLength) {
                this.Stop();
            }
        }     
    }

    public override void Dispose() {
        Raylib.UnloadMusicStream(music); 
        this.deleteTempFile();
        AudioManager.RemoveAudio(this);
    }

}

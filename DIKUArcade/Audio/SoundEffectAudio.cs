namespace DIKUArcade.Audio;

using Raylib_cs;
using System.Reflection;
using System;

public class SoundEffectAudio : Audio {

    private Sound sound;
    private Sound[] soundMulti;
    private int currentSound;
    private bool hasInitlized;

    public override bool IsPlaying { get => Raylib.IsSoundPlaying(sound); }

    public override float Volume { 
        get => volume;
        set {
            volume = Math.Clamp(value, 0.0f, 1.0f);
            Raylib_cs.Raylib.SetSoundVolume(sound, volume);
        }
    }

    public override float Pitch {
        get => pitch;
        set {
            pitch = Math.Clamp(value, 0.5f, 1.5f);
            Raylib.SetSoundPitch(sound, pitch);
        }
    }

    public SoundEffectAudio(string manifestResourceName)
        : base(manifestResourceName, Assembly.GetCallingAssembly()) {
        sound = Raylib.LoadSound(Path);
        Volume = volume;
        soundMulti = new Sound[10];
        currentSound = 0;
        hasInitlized = false;
    }   

    public override void Play() {
        Raylib.PlaySound(sound);
    }

    public override void Stop() {
        Raylib.StopSound(sound);
    }

    public override void Pause() {
        if (IsPlaying) {
            Raylib.PauseSound(sound);
        }
    }

    public override void Resume() {
        if (!IsPlaying) {
            Raylib.ResumeSound(sound);
        }

    }

    public override void Dispose() {
        if (hasInitlized) {
            for (int i = 1; i < soundMulti.Length; i++) {
                Raylib.UnloadSoundAlias(soundMulti[i]);
            }
        }
        Raylib.UnloadSound(sound); 
        this.deleteTempFile();
        AudioManager.RemoveAudio(this);
    }

    public void PlaySoundMulti(int size = 10) {
        Console.WriteLine(soundMulti.Length);
        if (!hasInitlized || soundMulti.Length < size) {
            createSoundAliases(size);
        }

        Raylib.PlaySound(soundMulti[currentSound]);
        currentSound++;

        if (currentSound >= size) {
            currentSound = 0;
        }
    }

    private void createSoundAliases(int size) {
        if (hasInitlized) {
            for (int i = 1; i < soundMulti.Length; i++) {
                Raylib.UnloadSoundAlias(soundMulti[i]);
            }
        }

        soundMulti = new Sound[size];
        soundMulti[0] = sound;
        for (int i = 1; i < soundMulti.Length; i++) {
            soundMulti[i] = Raylib.LoadSoundAlias(sound);
        }
        hasInitlized = true;
    }

}

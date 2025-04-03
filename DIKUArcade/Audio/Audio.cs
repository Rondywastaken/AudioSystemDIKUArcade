namespace DIKUArcade.Audio;

using System;
using System.IO;
using System.Reflection;

public abstract class Audio : IDisposable {

    protected float volume = 0.5f;
    protected float pitch = 1.0f;
    protected string Path { get; private set; }
    public bool Loop { get; set; }

    protected Audio(string manifestResourceName, Assembly assembly) {
        if (!AudioDevice.IsInitialized) {
            AudioDevice.OpenAudioDevice();
        }

        try {
            this.Path = getPath(assembly, manifestResourceName);
            this.Loop = false;
            AudioManager.AddAudio(this);
        } catch (Exception e) {
            if (this.Path != null) {
                this.deleteTempFile();
            }
            
            AudioManager.CleanUp();
            throw new Exception($"Problem creating Audio instance: {e.Message}");
        }
    }

    protected Audio(string manifestResourceName, Assembly assembly, bool loop) {
        if (!AudioDevice.IsInitialized) {
            AudioDevice.OpenAudioDevice();
        }

        try {
            this.Path = getPath(assembly, manifestResourceName);
            this.Loop = Loop;
            AudioManager.AddAudio(this);
        } catch (Exception e) {
            if (this.Path != null) {
                this.deleteTempFile();
            }
            
            AudioManager.CleanUp();
            throw new Exception($"Problem creating Audio instance: {e.Message}");
        }
       
    }

    private string getPath(Assembly assembly, string manifestResourceName) {
        using (Stream? stream = assembly.GetManifestResourceStream(manifestResourceName)) {
            if (stream is null) {
                throw new ArgumentNullException($"Resource: {manifestResourceName} does not exist. " +
                 "Make sure the name is correct and you have embedded the file using the .csproj" + 
                 "file.");
            }
                
            string[] splitString = manifestResourceName.Split(".");

            return createTempFile(stream, splitString[splitString.Length - 1]);
        }
    }

    private string createTempFile(Stream stream, string filetype) {
        // Create a temporary unique file path
        string tempFilePath = $"./{Guid.NewGuid()}.{filetype}";

        // Copy stream data to temporary file
        using(FileStream file = File.Create(tempFilePath)) {
            stream.CopyTo(file);
        }

        return tempFilePath;
    }

    protected void deleteTempFile() {
        File.Delete(Path);
    }

    ~Audio() {
        this.Dispose();
    }

    public abstract bool IsPlaying { get; }

    public abstract float Volume { get; set; }

    public abstract float Pitch { get; set; }

    public abstract void Play();

    public abstract void Stop();

    public abstract void Pause();

    public abstract void Resume();

    public abstract void Dispose();

}

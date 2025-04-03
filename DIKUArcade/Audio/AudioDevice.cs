namespace DIKUArcade.Audio;

using Raylib_cs;

public static class AudioDevice {
    public static bool IsInitialized { get; private set; }

    public static void OpenAudioDevice() {
        if (!IsInitialized) {
            Raylib.InitAudioDevice();
            Raylib.SetAudioStreamBufferSizeDefault(4096);
            IsInitialized = true;
        }
    }

    public static void CloseAudioDevice() {
        if (IsInitialized) {
            Raylib.CloseAudioDevice();
            IsInitialized = false;
        }
    }

}

namespace DIKUArcade.Audio;

using System;
using System.Collections.Generic;

public static class AudioManager {
    private static List<Audio> audioList = new List<Audio>(); 

    public static void AddAudio(Audio audio) {
        try {
            if (audioList.Contains(audio)) {
                throw new ArgumentException($"Audio: {audio} is already in the AudioManager");
            }
            audioList.Add(audio);
            Console.WriteLine("Added Audio");

        } catch {
            AudioManager.CleanUp();
            throw;
        }
    }

    public static void RemoveAudio(Audio audio) {
        if (audioList.Contains(audio)) {
            audioList.Remove(audio);
        }
    }

    public static void CleanUp() {
        while (audioList.Count > 0) {
            int index = audioList.Count - 1;
            audioList[index].Dispose();
        }
        AudioDevice.CloseAudioDevice();
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Music : GameObject
{
    public static float musicVolume = 1.0f;

    public Sound gameTrack;
    public Sound menuTrack;

    public SoundChannel musicSoundChannel;

    public Music() : base()
    {
        gameTrack = new Sound("assets/soundtrack/Pigeonsrevenge_final.mp3", true, true);
        menuTrack = new Sound("assets/soundtrack/Menumusic.mp3", true, true);
    }
}


public static class MusicStatic
{
    private static float _musicVolume;
    public static Sound gameTrack = new Sound("assets/soundtrack/Pigeonsrevenge_final.mp3", true, true);
    public static Sound menuTrack = new Sound("assets/soundtrack/Menumusic.mp3", true, true);

    public static SoundChannel musicSoundChannel;

    /// <summary>
    /// Sets the sound. Input volume as 0...1f.
    /// </summary>
    /// <param name="volume"></param>
    public static void SetMusicVolume(float volume)
    {
        _musicVolume = volume;
    }

    public static float GetMusicVolume()
    {
        return _musicVolume;
    }

    public static void PlayMusic(Sound sound)
    {
        musicSoundChannel = sound.Play();
        musicSoundChannel.Volume = _musicVolume;
    }
}

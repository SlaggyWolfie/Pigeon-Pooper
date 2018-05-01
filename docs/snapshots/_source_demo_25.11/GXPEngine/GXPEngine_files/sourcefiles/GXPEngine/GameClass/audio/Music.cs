using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Music
{
    public Sound gameTrack;
    public Sound menuTrack;

    public SoundChannel soundChannel;

    public Music()
    {
        gameTrack = new Sound("assets/soundtrack/Pigeonsrevenge_final.mp3", true, true);
        menuTrack = new Sound("assets/soundtrack/Menumusic.mp3", true, true);
    }
}

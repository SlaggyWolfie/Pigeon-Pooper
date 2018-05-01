using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public static class PauseManager
{
    public static bool pause = false;
    //false is for unpaused;

    public static void Pause()
    {
        pause = true;
    }
    public static void UnPause()
    {
        pause = false;
    }
    public static bool GetPause()
    {
        return pause;
    }

}

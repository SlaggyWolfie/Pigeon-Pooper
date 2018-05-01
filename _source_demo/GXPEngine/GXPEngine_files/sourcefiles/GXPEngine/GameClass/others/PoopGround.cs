using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class PoopGround : Sprite
{
    public PoopGround() : base("assets/sprites/groundpoop.png")
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.5f, 0.5f);
        rotation = Utils.Random(1, 361);
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        y += Difficulty.GetScrollSpeed();

        if (y >= game.height * 2)
        {
            Destroy();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Road : Sprite
{
    private Level _level;
    public Road(string filename, Level pLevel) : base(filename)
    {
        _level = pLevel;
        Rescale();
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        y += Difficulty.GetScrollSpeed(); //* Time.deltaTime;
        if (y >= game.height)
        {
            Destroy();
        }
    }

    private void Rescale()
    {
        float xScale = MyGame.OldX() / width;
        float yScale = MyGame.OldY() / height;
        SetScaleXY(xScale, yScale);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _level.roadList.Remove(this);
    }
}

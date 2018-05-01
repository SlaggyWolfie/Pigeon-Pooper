using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Enemy : Sprite
{
    const int SPEED = 5;
    private Level _level;

    public Enemy(Level pLevel) : base("enemytest.png")
    {
        SetOrigin(width / 2, height / 2);
        rotation = 180;
        //Note: 0.65f for scale reference.
        SetScaleXY(0.65f, 0.65f);
        _level = pLevel;

    }

    private void Update()
    {
        y += SPEED;
        if (y >= game.height + height)
        {
            Destroy();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _level.enemyList.Remove(this);
    }
}
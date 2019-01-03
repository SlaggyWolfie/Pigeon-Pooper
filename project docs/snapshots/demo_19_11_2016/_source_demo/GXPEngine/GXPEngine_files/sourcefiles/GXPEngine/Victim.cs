using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Victim : AnimationSprite
{
    private float _speed;
    private float _scrollSpeed;
    private Level _level;
    private Projectile _poop;
    public Victim(Level pLevel) : base("Victim.png", 2, 1)
    {
        currentFrame = 0;
        _scrollSpeed = MyGame.GetScrollSpeed();
        _speed = _scrollSpeed * 2;
        _level = pLevel;
        SetOrigin(width / 2, height / 2);
        //x = Utils.Random(width, game.width - width);
    }

    private void Update()
    {
        GotPoopedOn();
        //RoadSpeedModifier();
        GotOutOfTheWindow();
    }
   
    
    private void GotPoopedOn()
    {
        //Credits to Plamen Petrov for the help in rewriting this part.
        //Might have to rewrite this anyway.
        y += _speed;

        if (_level.poopList.Count > 0)
        {
            _poop = _level.poopList[0] as Projectile;

            if (HitTest(_poop))
            {
                if (_poop.GetScale() <= 0.2f)
                {
                    _poop.Destroy();
                    currentFrame = 1;
                    _speed = _scrollSpeed;
                    _level.player.score += 10;
                }
            }
        }
    }

    private void RoadSpeedModifier()
    {
        foreach (Road _road in _level.roadList)
        {
            if (HitTest(_road))
            {
                y += _speed;
            }
        }
    }

    private void GotOutOfTheWindow()
    {
        if (y >= game.height + height)
        {
            Destroy();
            _level.victimList.Remove(this);
        }
    }
}

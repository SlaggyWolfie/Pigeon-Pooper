using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Victim : AnimationSprite
{
    private bool _poopedOn;
    private float _speed;
    //private float _scrollSpeed;

    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s
    private int _lastUpdatedAnimationTime;

    private Level _level;
    private Projectile _poop;

    public Victim(Level pLevel) : base("human" + Utils.Random(1, 4) +".png", 4, 1)
    {
        currentFrame = 2;
        _poopedOn = false;
        //_scrollSpeed = MyGame.GetScrollSpeed();
        //_speed = _scrollSpeed * 2;
        _level = pLevel;
        SetOrigin(width / 2, height / 2);
        //x = Utils.Random(width, game.width - width);
    }

    private void Update()
    {
        GotPoopedOn();
        //RoadSpeedModifier();
        GotOutOfTheWindow();
        if (!_poopedOn) AdvanceAnimation();
    }
   
    
    private void GotPoopedOn()
    {
        //Credits to Plamen Petrov for the help in rewriting this part.
        //Might have to rewrite this anyway.
        _speed = Difficulty.GetScrollSpeed() * 2;
        y += _speed;

        if (_level.poopList.Count > 0)
        {
            _poop = _level.poopList[0] as Projectile;

            if (HitTest(_poop))
            {
                if (_poop.GetScale() <= 0.2f)
                {
                    _poop.Destroy();
                    currentFrame = 0;
                    SetColor(0.1f, 0.1f, 0.1f);
                    _speed = Difficulty.GetScrollSpeed() / 2;
                    _level.player.score += 10;
                    _poopedOn = true;
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

    private void AdvanceAnimation()
    {

        if (Time.time > ANIMATION_DELAY + _lastUpdatedAnimationTime)
        {
            currentFrame = OurUtils.ValueLooper(currentFrame, 0, 3);
            _lastUpdatedAnimationTime = Time.time;
        }
    }
}

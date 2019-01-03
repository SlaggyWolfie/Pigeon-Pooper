using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Victim : AnimationSprite
{
    public bool poopedOn;
    private float _speed;
    public string facing;
    //private string 
    //private float _scrollSpeed;

    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s
    private int _lastUpdatedAnimationTime;

    private Level _level;
    private Projectile _poop;

    public Victim(Level pLevel, string pFacing) : base(RandomAssets.GetRandomVictim(), 4, 1)
    {
        facing = pFacing;
        if (facing == "up") rotation = 180;
        if (facing == "down") rotation = 0;
        poopedOn = false;
        //_scrollSpeed = MyGame.GetScrollSpeed();
        //_speed = _scrollSpeed * 2;
        _level = pLevel;
        SetOrigin(width / 2, height / 2);
        //x = Utils.Random(width, game.width - width);
    }

    private void Update()
    {
        GotPoopedOn();
        GotOutOfTheWindow();
        if (!poopedOn) AdvanceAnimation();
    }
   
    
    private void GotPoopedOn()
    {
        //Credits to Plamen Petrov for the help in rewriting this part.
        //Might have to rewrite this anyway.
        _speed = Difficulty.GetScrollSpeed() * 2;
        if (facing == "up" && !poopedOn) _speed *= (-1);
        y += _speed;

        if (_level.poopList.Count > 0)
        {
            _poop = _level.poopList[0] as Projectile;

            if (HitTest(_poop))
            {
                if (_poop.GetScale() <= 0.3f)
                {
                    _poop.Destroy();
                    currentFrame = 0;
                    SetColor(0.1f, 0.1f, 0.1f);
                    _speed = Difficulty.GetScrollSpeed() / 2;
                    _level.player.score += 10;
                    poopedOn = true;
                }
            }
        }
    }

    private void RoadSpeedModifier()
    {
        //Not Used currently.
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
        if (y >= game.height + game.height || y <= 0 - game.height * 2)
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

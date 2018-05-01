using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Projectile : Sprite
{
    private float _distanceFromGround;
    private float _scrollSpeed;
    private float _scale;
    private Level _level;
    

    //Codename: (pidgeon) poop
    public Projectile(Level pLevel) : base("assets/placeholders/circle.png")
    {
        //Initialization of variables
        _level = pLevel;
        _distanceFromGround = 0.0f;
        _scrollSpeed = 0.5f; //Difficulty.GetScrollSpeed();
        _scale = 0.8f;
        SetOrigin(width / 2, height / 2);
    }

    private void Update()
    {
        //Adding scroll speed to the Y to imitate scrolling
        y += _scrollSpeed;
        //Scaling down - imitating falling
        _distanceFromGround += 0.01f;
        _scale = 0.8f - _distanceFromGround;
        SetScaleXY(_scale, _scale);
        if (_scale <= 0.2f)
        {
            Destroy();
        }
    }

    private void JustTestStuffThatWorkedInACoolWay()
    {
        //Not used currently.
        _distanceFromGround += 0.01f;
        _scale -= _distanceFromGround;
        SetScaleXY(_scale, _scale);
        if (_scale <= 0)
        {
            Destroy();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _level.poopList.Remove(this);
    }

    public float GetScale()
    {
        return _scale;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Projectile : AnimationSprite
{
    const double SCORE_PER_MISS = 10;
    private float _distanceFromGround;
    private float _scrollSpeed;
    private float _scale;
    private float _startingScale;
    private Level _level;

    //Milliseconds
    const int ANIMATION_DELAY = 350; // 0.25s

    private int _lastUpdatedAnimationTime;
    
    //Codename: (pidgeon) poop
    public Projectile(Level pLevel) : base("assets/animation_sprites/bluepoop.png", 2, 1)
    {
        //Initialization of variables
        _level = pLevel;
        _distanceFromGround = 0.0f;
        _scrollSpeed = 0.5f; //Difficulty.GetScrollSpeed();
        _startingScale = 1.2f;
        _scale = _startingScale;
        SetOrigin(width / 2, height / 2);
    }

    private void Update()
    {
        //Adding scroll speed to the Y to imitate scrolling
        y += _scrollSpeed;
        //Scaling down - imitating falling
        _distanceFromGround += 0.015f;
        _scale = _startingScale - _distanceFromGround;
        SetScaleXY(_scale, _scale);
        if (_scale <= 0.3f)
        {
            Destroy();
            SFX.PlaySound(SFX.killEnemy);
            _level.player.score += SCORE_PER_MISS * Difficulty.GetScoreModifier();
            _level.CreateGroundPoop(x, y);
        }
        AdvanceAnimation();
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

    private void AdvanceAnimation()
    {
        if (Time.time > ANIMATION_DELAY + _lastUpdatedAnimationTime)
        {
            //currentFrame = OurUtils.ValueBouncer(currentFrame, 0, 2);
            currentFrame = OurUtils.ValueCheckerAndAlternator(currentFrame, 0, 1, true);
            _lastUpdatedAnimationTime = Time.time;
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

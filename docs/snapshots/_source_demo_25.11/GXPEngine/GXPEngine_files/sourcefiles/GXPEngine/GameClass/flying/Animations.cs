﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class PlayerAnimation : AnimationSprite
{
    public static bool gotHit;
    //Milliseconds
    const int ANIMATION_DELAY = 170; // 0.25s
    const int GLIDE_TIME = 3000; //3s

    private int _lastUpdatedAnimationTime;
    private int _actualAnimationDelay;
    private int _birdFlap;
    private Player _player;

    public PlayerAnimation(Player pPlayer) : base("assets/animation_sprites/playerSheet.png", 5, 1)
    {
        SetOrigin(width / 2, height / 2);
        _player = pPlayer;
        currentFrame = 0;
        _lastUpdatedAnimationTime = 0;
        _actualAnimationDelay = ANIMATION_DELAY;
        _birdFlap = 0;
        gotHit = false;
        //rotation = -90;
        //SetColor(1, 0, 0);
        //color = 0x99f4f8;
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        AdvanceBirdAnimation();
    }

    private void AdvanceAnimation()
    {
        if (Time.time > ANIMATION_DELAY + _lastUpdatedAnimationTime)
        {
            //currentFrame = OurUtils.ValueBouncer(currentFrame, 0, 2);
            currentFrame = OurUtils.ValueLooper(currentFrame, 0, 3);
            _lastUpdatedAnimationTime = Time.time;
        }
    }
    private void AdvanceBirdAnimation()
    {
        _actualAnimationDelay = ANIMATION_DELAY;
        /*
        if ()
        {
            _actualAnimationDelay += 1000;
        }*/
        if (currentFrame == 0 && _birdFlap % 3 == 0)
        {
            _actualAnimationDelay = GLIDE_TIME;
        }
        if (Time.time > _actualAnimationDelay + _lastUpdatedAnimationTime)
        {
            //currentFrame = OurUtils.ValueBouncer(currentFrame, 0, 2);
            currentFrame = OurUtils.ValueLooper(currentFrame, 0, 3);
            _lastUpdatedAnimationTime = Time.time;

            _birdFlap++;
        }
    }
    
    public bool GetHit(bool doCheck)
    {
        currentFrame = 4;
        if (!doCheck) gotHit = true;
        return gotHit;
    }
    //public voi
}

public class EnemyAnimation : AnimationSprite
{
    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s
    const int GLIDE_TIME = 2000; //2s

    private int _lastUpdatedAnimationTime;
    private int _actualAnimationDelay;
    private int _birdFlap;

    private Enemy _enemy;

    public EnemyAnimation(Enemy pEnemy) : base("assets/animation_sprites/enemyspritesheet.png", 5, 1)
    {
        SetOrigin(width / 2, height / 2);
        _enemy = pEnemy;
        currentFrame = 0;
        _lastUpdatedAnimationTime = 0;
        _actualAnimationDelay = ANIMATION_DELAY;
        _birdFlap = 0;
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        AdvanceBirdAnimation();
    }

    private void AdvanceAnimation()
    {
        if (Time.time > ANIMATION_DELAY + _lastUpdatedAnimationTime)
        {
            //currentFrame = OurUtils.ValueBouncer(currentFrame, 0, 2);
            currentFrame = OurUtils.ValueLooper(currentFrame, 0, 3);
            _lastUpdatedAnimationTime = Time.time;
        }
    }

    private void AdvanceBirdAnimation()
    {
        _actualAnimationDelay = ANIMATION_DELAY;
        if (currentFrame == 0 && _birdFlap % 3 == 0)
        {
            _actualAnimationDelay = GLIDE_TIME;
        }
        if (Time.time > _actualAnimationDelay + _lastUpdatedAnimationTime)
        {
            //currentFrame = OurUtils.ValueBouncer(currentFrame, 0, 2);
            currentFrame = OurUtils.ValueLooper(currentFrame, 0, 3);
            _lastUpdatedAnimationTime = Time.time;

            _birdFlap++;
        }
    }

    public void GetHit()
    {
        currentFrame = 4;
    }
}

public class VictimAnimation : AnimationSprite
{
    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s

    private int _lastUpdatedAnimationTime;

    private Victim _victim;

    public VictimAnimation(Victim pVictim) : base(GetAssets.GetRandomVictim(), 4, 1)
    {
        SetOrigin(width / 2, height / 2);
        _victim = pVictim;
        currentFrame = 0;
        _lastUpdatedAnimationTime = 0;
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        if (!_victim.poopedOn) AdvanceAnimation();
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
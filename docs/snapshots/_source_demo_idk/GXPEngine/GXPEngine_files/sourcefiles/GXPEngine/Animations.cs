using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class PlayerAnimation : AnimationSprite
{
    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s

    private int _lastUpdatedAnimationTime;

    private Player _player;

    public PlayerAnimation(Player pPlayer) : base("assets/animation_sprites/playerSheet.png", 4, 1)
    {
        SetOrigin(width / 2, height / 2);
        _player = pPlayer;
        currentFrame = 0;
        _lastUpdatedAnimationTime = 0;
        
        //rotation = -90;
        //SetColor(1, 0, 0);
        //color = 0x99f4f8;
    }

    private void Update()
    {
        AdvanceAnimation();
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
}

public class EnemyAnimation : AnimationSprite
{
    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s

    private int _lastUpdatedAnimationTime;

    private Enemy _enemy;

    public EnemyAnimation(Enemy pEnemy) : base("assets/animation_sprites/enemyspritesheet.png", 4, 1)
    {
        SetOrigin(width / 2, height / 2);
        _enemy = pEnemy;
        currentFrame = 0;
        _lastUpdatedAnimationTime = 0;
    }

    private void Update()
    {
        AdvanceAnimation();
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
}

public class VictimAnimation : AnimationSprite
{
    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s

    private int _lastUpdatedAnimationTime;

    private Victim _victim;

    public VictimAnimation(Victim pVictim) : base(RandomAssets.GetRandomVictim(), 4, 1)
    {
        SetOrigin(width / 2, height / 2);
        _victim = pVictim;
        currentFrame = 0;
        _lastUpdatedAnimationTime = 0;
    }

    private void Update()
    {
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
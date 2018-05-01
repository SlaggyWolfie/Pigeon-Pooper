using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Enemy : Sprite
{
    public EnemyAnimation enemyAnimation;
    const float SPEED = 0.5f;
    private Level _level;

    public Enemy(Level pLevel) : base("assets/sprites/enemyhitbox.png")
    {
        SetOrigin(width / 2, height / 2);
        rotation = 180;
        //Note: 0.65f for scale reference.
        SetScaleXY(0.65f, 0.65f);
        _level = pLevel;

        enemyAnimation = new EnemyAnimation(this);
        AddChild(enemyAnimation);
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        y += SPEED * Time.deltaTime;
        if (y >= game.height + height)
        {
            Destroy();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_level.player.canKill) _level.player.score += _level.player.birdKillPoints;
        _level.enemyList.Remove(this);
    }
}
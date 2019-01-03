using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Player : AnimationSprite
{
    const int SPEED = 5;
    const int ON_HIT_ENERGY_LOSS = 60;
    const int MAX_ENERGY = 100;
    const int MIN_ENERGY = 0;
    const int ENERGY_PER_DELAY = 5;

    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s
    const int UPDATE_SCORE_DELAY = 1000; //1s
    const int UPDATE_ENERGY_DELAY = 1000; //1s

    private int _lastUpdatedScoreTime;
    private int _lastUpdatedEnergyTime;
    private int _lastUpdatedAnimationTime;

    private bool _isHit;
    private int _hit;
    
    private Level _level;

    public int score;
    public int energy;

    public Player(Level pLevel) : base("playerSheet.png", 4, 1)
    {
        //Sprite Initialization
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.5f, 0.5f);
        //rotation = -90;
        //SetColor(1, 0, 0);
        //color = 0x99f4f8;
        currentFrame = 0;

        //Timer initialization
        _lastUpdatedScoreTime = 0;
        _lastUpdatedEnergyTime = 0;
        _lastUpdatedAnimationTime = 0;

        //Enemy Collision variables initialization
        _isHit = false;
        _hit = 0;

        //Level Initialization
        _level = pLevel;

        //Initialization of other variables
        energy = MAX_ENERGY;
        score = 0;
    }
    
    private void Update()
    {
        AdvanceAnimation();
        EnergyRegen();
        UpdateScore();
        MovementControls();
        OtherControls();
        EnemyCollision();
        HitCheck();
    }

    private void MovementControls()
    {
        if (Input.GetKey(Key.LEFT))
        {
            TryMove(-SPEED, 0);
        }
        if (Input.GetKey(Key.RIGHT))
        {
            TryMove(SPEED, 0);
        }
        if (Input.GetKey(Key.UP))
        {
            TryMove(0, -SPEED);
        }

        if (Input.GetKey(Key.DOWN))
        {
            TryMove(0, SPEED);
        }
    }

    private void OtherControls()
    {

        if (Input.GetKeyDown(Key.ESCAPE))
        {
            game.Destroy();
        }
        if (Input.GetKeyDown(Key.SPACE))
        {
            //Energy for pooping
            energy -= 10;

            _level.CreatePoop();
        }

        if (Input.GetKey(Key.R))
        {

        }
    }

    private void TryMove(float moveX, float moveY)
    {
        x += moveX;
        y += moveY;

        if (x <= width / 2 || x >= (game.width - width / 2) || y <= height / 2 || y >= (game.height - height / 2))
        {
            x = x - moveX;
            y = y - moveY;
            return;
        }

        //Wall Collision code
        //might still be used later
        /*
        else if (Wall.wallLength[0] != null) //|| _level.enemyList[0] != null)
        {
            foreach (GameObject wall in Wall.wallLength)
            {
                if (HitTest(wall))
                {
                    x -= moveX;
                    y -= moveY;
                    //_speedY = 0;
                    return;
                }
            }
        } */
    }

    private void EnemyCollision()
    {
        //if (_level.enemyList[0] != null) && _level.enemyList.Count >= 0)
        if (_level.enemyList.Count >= 0)
        {
            foreach (Enemy enemy in _level.enemyList)
            {
                //Just wanted to be double sure
                if (HitTest(enemy))
                {
                    _isHit = true;
                    _hit++;
                }
                
                if (!HitTest(enemy))
                {
                    _isHit = false;
                    _hit = 0;
                }
            }
        }
    }

    private void UpdateScore()
    {
        if (Time.time > UPDATE_SCORE_DELAY + _lastUpdatedScoreTime)
        {
            score++;
            _lastUpdatedScoreTime = Time.time;
        }
    }

    private void EnergyRegen()
    {
        //Needed because no power ups for now
        if (Time.time > UPDATE_ENERGY_DELAY + _lastUpdatedEnergyTime)
        {
            energy += ENERGY_PER_DELAY;
            if (energy >= MAX_ENERGY) energy = MAX_ENERGY;
            _lastUpdatedEnergyTime = Time.time;
        }
        //At some point this will have a detrimental effect on the energy after power-ups.
    }

    private void DeathCheck()
    {
        if (energy <= 0)
        {
            //Nothing yet
        }
    }

    private void HitCheck()
    {
        if (_isHit && _hit == 1)
        {
            energy -= ON_HIT_ENERGY_LOSS;
        }
    }

    private void AdvanceAnimation()
    {
        if (Time.time > ANIMATION_DELAY + _lastUpdatedAnimationTime)
        {
            currentFrame = OurUtils.ValueBouncer(currentFrame, 0, 2);
            _lastUpdatedAnimationTime = Time.time;
        }
    }

    private void BirdAnimation()
    {
        //Not used currently. Hasn't been updated to new standards. Refer to AdvanceAnimation()
        currentFrame = OurUtils.ValueCheckerAndAlternator(currentFrame, 0, 1, true);
        Timer fly = new Timer(250, BirdAnimation);
    }
}

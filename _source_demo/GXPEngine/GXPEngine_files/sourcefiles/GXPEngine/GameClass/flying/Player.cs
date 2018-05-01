using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.OpenGL;
using GXPEngine.Core;
public class Player : Sprite
{
    const float STARTING_SPEED = 5.0f;
    const float SPEED = 0.1f;
    const int ON_HIT_ENERGY_LOSS = 60;
    const int MAX_ENERGY = 100;
    const int MIN_ENERGY = 0;
    const int ENERGY_PER_DELAY = -1;
    const double SCORE_PER_SECOND = 1;
    const double SCORE_PER_KILL = 1000;

    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s
    const int UPDATE_SCORE_DELAY = 1000; //1s
    const int UPDATE_ENERGY_DELAY = 1000; //1s

    private int _lastUpdatedScoreTime;
    private int _lastUpdatedEnergyTime;

    private bool _isHit;
    private int _hit;

    private Level _level;
    PlayerAnimation _playerAnimation;

    public double score;
    public float speed;
    public int energy;
    public int poopCost;
    public int startingPoopCost;
    public double birdKillPoints;

    public bool canPoop;
    public bool canKill;

    public float speedX;
    public float speedY;

    public Player(Level pLevel) : base("assets/sprites/playerhitbox.png")
    {
        //Sprite Initialization
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.5f, 0.5f);

        //Timer initialization
        _lastUpdatedScoreTime = 0;
        _lastUpdatedEnergyTime = 0;

        //Enemy Collision variables initialization
        _isHit = false;
        _hit = 0;

        //Level Initialization
        _level = pLevel;

        //Initialization of other variables
        speed = STARTING_SPEED;
        score = 0;
        energy = MAX_ENERGY;
        startingPoopCost = 10;
        poopCost = startingPoopCost;
        birdKillPoints = SCORE_PER_KILL;

        canPoop = true;
        canKill = false;

        _playerAnimation = new PlayerAnimation(this);
        AddChild(_playerAnimation);


    }

    static bool _music = true;
    public static bool GetMusic()
    {
        return _music;
    }



    private void Update()
    {
        if (PauseManager.GetPause()) return;
        EnergyRegen();
        if (energy >= MAX_ENERGY) energy = MAX_ENERGY;
        if (energy <= MIN_ENERGY) energy = MIN_ENERGY;
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
            speedX -= SPEED * Time.deltaTime;
        }
        if (Input.GetKey(Key.RIGHT))
        {
            speedX += SPEED * Time.deltaTime;
        }
        if (Input.GetKey(Key.UP))
        {
            speedY -= SPEED * Time.deltaTime;
        }

        if (Input.GetKey(Key.DOWN))
        {
            speedY += SPEED * Time.deltaTime;
        }

        TryMove(speedX, 0);
        TryMove(0, speedY);

        speedX = speedX * 0.9f;
        speedY = speedY * 0.9f;
    }

    private void OtherControls()
    {

        // if (Input.GetKeyDown(Key.ESCAPE))
        // {
        //     game.Destroy();
        // }
        if (Input.GetKeyDown(Key.SPACE))
        {
            if (canPoop)
            {
                SFX.PlaySound(SFX.poopSound);
                //Energy for pooping
                energy -= poopCost;

                _level.CreatePoop();
            }
        }

        if (Input.GetKeyDown(Key.R))
        {
            //score += 5000;
        }


        if (Input.GetKeyDown(Key.P))
        {
            if (HUD.barOrMeter == true)
            {
                HUD.barOrMeter = false;
            }
            else if (HUD.barOrMeter == false)
            {
                HUD.barOrMeter = true;
            }
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
        if (_level.enemyList.Count >= 0)
        {
            foreach (Enemy enemy in _level.enemyList)
            {
                //Just wanted to be double sure
                if (HitTest(enemy))
                {
                    if (canKill)
                    {
                        enemy.enemyAnimation.gotHit = true;
                        Timer enemyDeath = new Timer(150, enemy.Destroy);
                    }
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

    private void HitCheck()
    {
        if (_isHit && _hit == 1)
        {
            if (canKill)
            {
                SFX.PlaySound(SFX.killEnemy);
            }
            if (!canKill)
            {
                _playerAnimation.gotHit = true;
                SFX.PlaySound(SFX.death);
                energy -= ON_HIT_ENERGY_LOSS;
            }
        }
    }

    private void UpdateScore()
    {
        if (Time.time > UPDATE_SCORE_DELAY + _lastUpdatedScoreTime)
        {
            score += SCORE_PER_SECOND * Difficulty.GetScoreModifier();
            score = Math.Floor(score);
            _lastUpdatedScoreTime = Time.time;
        }
    }

    private void EnergyRegen()
    {
        //Needed because no power ups for now
        if (Time.time > UPDATE_ENERGY_DELAY + _lastUpdatedEnergyTime)
        {
            energy += ENERGY_PER_DELAY;
            //if (energy >= MAX_ENERGY) energy = MAX_ENERGY;
            _lastUpdatedEnergyTime = Time.time;
        }
        //At some point this will have a detrimental effect on the energy after power-ups.
    }

}

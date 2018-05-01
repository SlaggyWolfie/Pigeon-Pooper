using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.OpenGL;
using GXPEngine.Core;
public class Player : Sprite
{
    const float SPEED = 5;
    const int ON_HIT_ENERGY_LOSS = 60;
    const int MAX_ENERGY = 100;
    const int MIN_ENERGY = 0;
    const int ENERGY_PER_DELAY = 5;
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

    public bool canPoop;
    public bool canKill;

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
        speed = SPEED;
        score = 0;
        energy = MAX_ENERGY;
        startingPoopCost = 10;
        poopCost = startingPoopCost;

        canPoop = true;
        canKill = false;

        _playerAnimation = new PlayerAnimation(this);
        AddChild(_playerAnimation);
    }
    
    private void Update()
    {
        //AdvanceAnimation();
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
            TryMove(-speed, 0);
        }
        if (Input.GetKey(Key.RIGHT))
        {
            TryMove(speed, 0);
        }
        if (Input.GetKey(Key.UP))
        {
            TryMove(0, -speed);
        }

        if (Input.GetKey(Key.DOWN))
        {
            TryMove(0, speed);
        }
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
                //Energy for pooping
                energy -= poopCost;

                _level.CreatePoop();
            }

            Console.WriteLine(WindowSize.instance.width);

            Console.WriteLine(game.width);
        }

        if (Input.GetKeyDown(Key.R))
        {/*
            Console.WriteLine(game.width);
            Console.WriteLine(game.height);
            float xScale = (float)game.width / 1920;
            float yScale = (float)game.height / 1080;
            game.SetScaleXY(xScale, yScale);
            
            Console.WriteLine(game.width);
            Console.WriteLine(game.height); */
            //game.SetViewport(0, 0, 1920, 1080)
            float xScale = 1920 / (float)game.width;
            float yScale = 1080 / (float)game.height;
            game.SetScaleXY(xScale, yScale); ;
            GL.glfwSetWindowSize(1920, 1080);
            Console.WriteLine(WindowSize.instance.width);
            //WindowSize.instance.width;
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
                    if (canKill)
                    {
                        //Timer enemyDeath = new Timer(50, enemy.Destroy);
                        //enemy.Destroy();
                        score += SCORE_PER_KILL * Difficulty.GetScoreModifier();
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
            energy -= ON_HIT_ENERGY_LOSS;
        }
    }

    private void UpdateScore()
    {
        if (Time.time > UPDATE_SCORE_DELAY + _lastUpdatedScoreTime)
        {
            score += SCORE_PER_SECOND * Difficulty.GetScoreModifier();
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
}

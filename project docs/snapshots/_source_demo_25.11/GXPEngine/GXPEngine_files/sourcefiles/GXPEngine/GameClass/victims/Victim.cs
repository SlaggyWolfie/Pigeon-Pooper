using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Victim : AnimationSprite
{
    const float COLLISION_OFFSET = 25;
    const double SCORE_PER_HIT = 300;
    const float SPEED_MULTIPLIER = 2f;
    const float VARIABLE_SPEED_MODIFIER = 0.3f;
    private float _speed;
    private float _startingRotation;
    private float _startingX;
    private float _startingY;
    private bool _gotCoords;
    private float _varSpeed;
    //private float _scrollSpeed;

    //Milliseconds
    const int ANIMATION_DELAY = 250; // 0.25s
    private int _lastUpdatedAnimationTime;

    private Level _level;
    private Projectile _poop;

    public bool poopedOn;
    public string facing;

    public Victim(Level pLevel, string pFacing) : base(GetAssets.GetRandomVictim(), 5, 1)
    {
        facing = pFacing;
        if (facing == "up") rotation = 180;
        if (facing == "down") rotation = 0;
        _startingRotation = rotation;
        _gotCoords = false;
        poopedOn = false;
        _varSpeed = Utils.Random(-VARIABLE_SPEED_MODIFIER, VARIABLE_SPEED_MODIFIER);
        _level = pLevel;
        SetOrigin(width / 2, height / 2);
        //VictimAnimation victimAnimation = new VictimAnimation(this);
        //AddChild(victimAnimation);
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        GetStartingCoordinates();
        NormalRotation();
        GotPoopedOn();
        GotOutOfTheWindow();
        CollideWithOtherVictim();
        if (!poopedOn) AdvanceAnimation();
    }
   
    
    private void GotPoopedOn()
    {
        //Credits to Plamen Petrov for the help in rewriting this part.
        //Might have to rewrite this anyway.
        _speed = (Difficulty.scrollSpeed + _varSpeed) * SPEED_MULTIPLIER; //* Time.deltaTime;
        if (facing == "up" && !poopedOn) _speed *= (-1);
        y += _speed;

        if (_level.poopList.Count > 0)
        {
            _poop = _level.poopList[0] as Projectile;

            if (HitTest(_poop))
            {
                if (_poop.GetScale() <= 0.4f)
                {
                    SFX.PlaySound(SFX.getHit);
                    _poop.Destroy();
                    currentFrame = 4;
                    //SetColor(0.1f, 0.1f, 0.1f);
                    _speed = Difficulty.GetScrollSpeed() / 2;
                    _level.player.score += SCORE_PER_HIT;
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

    private void CollideWithOtherVictim()
    {
        if (_level.victimList.Count > 0)
        {
            foreach (Victim victim in _level.victimList)
            {
                if (HitTest(victim))
                {
                    if (facing != victim.facing)
                    {
                        x = _startingX + COLLISION_OFFSET;
                        victim.x = _startingX - COLLISION_OFFSET;
                        rotation = _startingRotation + 90;
                    }
                    if (facing == victim.facing & victim != this)
                    {
                        x = _startingX + COLLISION_OFFSET;
                        victim.x = _startingX - COLLISION_OFFSET;
                        rotation = _startingRotation + 90;
                    }
                }
            }
        }
    }

    private void NormalRotation()
    {
        rotation = _startingRotation;
        x = _startingX;
    }

    private void GotOutOfTheWindow()
    {
        if (y >= game.height + game.height || y <= 0 - game.height * 2)
        {
            Destroy();
            _level.victimList.Remove(this);
        }
    }
    
    private void GetStartingCoordinates()
    {
        if (!_gotCoords)
        {
            _startingX = x;
            _startingY = y;
            _gotCoords = true;
        }
    }
    
    public float Boardwalk(float pX)
    {
        if (OurUtils.RandomBool())
        {
            //Left boardwalk
            pX = Utils.Random(width * 2, width * 4 - width / 2);
        }
        else
        {
            //Right boardwalk
            pX = Utils.Random(game.width - width * 4 + width / 2, game.width - width * 2);
        }
        return pX;
    }


    public void SetPositionVictim(Victim pVictim, float pX, float pY)
    {
        pVictim.SetXY(pX, pY);
        if (_level.victimList.Count > 0)
        {
            foreach (Victim otherVictim in _level.victimList)
            {
                if (pVictim.HitTest(otherVictim))
                {
                    SetPositionVictim(pVictim, pVictim.Boardwalk(pX), pY);
                }
            }
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

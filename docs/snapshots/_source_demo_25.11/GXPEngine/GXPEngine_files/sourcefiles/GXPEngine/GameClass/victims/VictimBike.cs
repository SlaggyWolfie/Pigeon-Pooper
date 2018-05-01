using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class VictimBike : AnimationSprite
{
    const double SCORE_PER_HIT = 200;
    const float SPEED_MULTIPLIER = 5f;
    private float _speed;
    private float _startingRotation;
    private float _startingX;
    private float _startingY;
    private bool _gotCoords;
    //private float _scrollSpeed;
    private float _sidewalk;

    private Level _level;
    private Projectile _poop;

    public bool poopedOn;
    public string facing;

    public VictimBike(Level pLevel, string pFacing) : base(GetAssets.GetRandomMotorBike(), 2, 1)
    {
        SetOrigin(width / 2, height / 2);
        facing = pFacing;
        if (facing == "up") rotation = 180;
        if (facing == "down") rotation = 0;
        _startingRotation = rotation;
        _gotCoords = false;
        poopedOn = false;
        _sidewalk = 190;
        _level = pLevel;
        currentFrame = 0;
    }
    private void Update()
    {
        if (PauseManager.GetPause()) return;
        GetStartingCoordinates();
        GotPoopedOn();
        GotOutOfTheWindow();
    }


    private void GotPoopedOn()
    {
        _speed = Difficulty.GetScrollSpeed() * SPEED_MULTIPLIER;// * Time.deltaTime;
        if (facing == "up") _speed *= (-1);
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
                    currentFrame = 1;
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

    }

    private void GotOutOfTheWindow()
    {
        if (y >= game.height + game.height || y <= 0 - game.height * 2)
        {
            Destroy();
            _level.bikeList.Remove(this);
        }
    }

    public float CorrectLane(float pX, string pFacing)
    {

        if (pFacing == "up")
        {
            //Right Lane
            pX = MyGame.OldX() / 2 + _sidewalk + width / 2;
        }
        if (pFacing == "down")
        {
            //Left Lane
            pX = MyGame.OldX() / 2 - _sidewalk - width / 2;
        }
        return pX;
    }


    public void SetPositionBike(VictimBike pBike, float pX, float pY)
    {
        pBike.SetXY(pX, pY);
        if (_level.bikeList.Count > 0)
        {
            foreach (VictimBike otherBike in _level.bikeList)
            {
                if (pBike.HitTest(otherBike))
                {
                    SetPositionBike(pBike, pBike.CorrectLane(pX, pBike.facing), pY);
                }
            }
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


}

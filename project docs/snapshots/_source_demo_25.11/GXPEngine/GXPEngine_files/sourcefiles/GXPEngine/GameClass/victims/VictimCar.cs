using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class VictimCar : AnimationSprite
{
    const double SCORE_PER_HIT = 100;
    const float SPEED_MULTIPLIER = 4f;
    private float _speed;
    private float _startingRotation;
    private float _startingX;
    private float _startingY;
    private bool _gotCoords;
    private float _strapsLength;

    private Level _level;
    private Projectile _poop;

    public bool poopedOn;
    public string facing;

    public VictimCar(Level pLevel, string pFacing) : base (GetAssets.GetRandomCar(), 2, 1)
    {
        SetOrigin(width / 2, height / 2);
        facing = pFacing;
        if (facing == "up") rotation = 180;
        if (facing == "down") rotation = 0;
        _startingRotation = rotation;
        _gotCoords = false;
        poopedOn = false;
        _strapsLength = 25;
        _level = pLevel;
        currentFrame = 0;
    }
    private void Update()
    {
        if (PauseManager.GetPause()) return;
        GetStartingCoordinates();
        //NormalRotation();
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
                    //SetColor(0.1f, 0.1f, 0.1f);
                    _level.player.score += SCORE_PER_HIT;
                    poopedOn = true;
                }
            }
        }
    }

    private void GotOutOfTheWindow()
    {
        if (y >= game.height + game.height || y <= 0 - game.height * 2)
        {
            Destroy();
            _level.carsList.Remove(this);
        }
    }

    public float CorrectLane(float pX, string pFacing)
    {
        
        if (pFacing == "up")
        {
            //Right Lane
            pX = MyGame.OldX() / 2 + _strapsLength + width / 2;
        }
        if (pFacing == "down")
        {
            //Left Lane
            pX = MyGame.OldX() / 2 - _strapsLength - width / 2;
        }
        return pX;
    }


    public void SetPositionCar(VictimCar pCar, float pX, float pY)
    {
        pCar.SetXY(pX, pY);
        if (_level.carsList.Count > 0)
        {
            foreach (VictimCar otherCar in _level.carsList)
            {
                if (pCar.HitTest(otherCar))
                {
                    SetPositionCar(pCar, pCar.CorrectLane(pX, pCar.facing), pY);
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

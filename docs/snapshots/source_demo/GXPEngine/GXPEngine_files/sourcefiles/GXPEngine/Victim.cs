using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Victim : AnimationSprite
{
    private float _speed;
    private Projectile _poop;
    private Level _level;
    public Victim(Level pLevel) : base("Victim.png", 2, 1)
    {
        currentFrame = 0;
        _speed = 1f;
        _level = pLevel;
        SetOrigin(width / 2, height / 2);
        x = Utils.Random(width, game.width - width);
    }

    private void Update()
    {
        GotPoopedOn();
        GotOutOfTheWindow();
    }
   
    
    private void GotPoopedOn()
    {
        //Credits to Plamen Petrov for the help in rewriting this par.
        y += _speed;

        if (_level.poopList.Count > 0)
        {
            _poop = _level.poopList[0] as Projectile;

            if (HitTest(_poop))
            {
                if (_poop.GetScale() <= 0.2f)
                {
                    _poop.Destroy();
                    currentFrame = 1;
                    _speed = 0.5f;
                }
            }
        }
    } 
    private void GotOutOfTheWindow()
    {
        if (y >= game.height + height)
        {
            Destroy();
            _level.victimList.Remove(this);
        }
    }
}

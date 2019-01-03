using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Player : Sprite
{
    const int SPEED = 5;

    //Level _level = new Level();

    public Player() : base("triangle.png")
    {
        SetOrigin(width / 2, height / 2);
        rotation = -90;
        //_level = pLevel;
    }
    
    private void Update()
    {
        MovementControls();
        OtherControls();
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

        if (Input.GetKey(Key.ESCAPE))
        {
            game.Destroy();
        }
        if (Input.GetKeyDown(Key.SPACE))
        {
            //Code has been moved to Level.cs
            //Reason being creating a reference to the level here would cause a stack overflow.
            //Projectile newPoop = new Projectile();
            //AddChild(newPoop);
            //newPoop.SetXY(x, y);
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
        /*
        else if (_level.wallList[0] != null || _level.enemyList[0] != null)
        {
            foreach (GameObject wall in _level.wallList)
            {
                if (HitTest(wall))
                {
                    x -= moveX;
                    y -= moveY;
                    _speedY = 0;
                    return;
                }

            }
        } */

    }
}

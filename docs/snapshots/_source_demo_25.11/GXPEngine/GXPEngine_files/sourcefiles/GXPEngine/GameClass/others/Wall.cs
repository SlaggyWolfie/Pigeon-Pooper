using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Wall : Sprite
{
    private float _scrollSpeed;
    private int _tileSize;

    private int _length;
    private float _positionX;
    private float _positionY;
    public static List<Sprite> wallLength = new List<Sprite> { };

    private Level _level;

    public Wall(Level pLevel) : base("assets/placeholders/white.png")
    {
        _scrollSpeed = Difficulty.GetScrollSpeed();
        _tileSize = Difficulty.GetTileSize();
        _level = pLevel;
        _length = Utils.Random(3, 6);
        
        //false (x = 0), true (x = game.width)
        //_positionX = OurUtils.RandomBool() ? game.width : 0;
        bool test = OurUtils.RandomBool();
        if (test)
        {
            SetOrigin(width, 0);
            _positionX = game.width;
        }
        else
        {
            SetOrigin(0, 0);
            _positionX = 0;
        }
        _positionY = Utils.Random(0, game.height) - game.height;

        x = _positionX;
        y = _positionY;
        //Console.WriteLine(x + " " + y);
        //Console.WriteLine(_positionX + " " + _positionY);
        CreateWall(_length, _positionX, _positionY);
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        PlayerCollision(wallLength);
        //_positionY += _scrollSpeed;
        y += _scrollSpeed;
        if (y >= game.height + _tileSize)
        {
            Destroy();
        }
        //Console.WriteLine(x + " " + y);
    }

    private void CreateWall(int pLength, float pX, float pY)
    {
        Sprite newWallTile = new Sprite("square.png");
        AddChild(newWallTile);
        Console.WriteLine(pX + " " + pY);
        newWallTile.SetXY(pX, pY);
        wallLength.Add(newWallTile);
        pLength--;

        if (_positionX > 0)
        {
            SetScaleXY(-1, 1);
            pX -= _tileSize;
        }
        else
        {
            pX += _tileSize;
        }
        if (pLength > 0)
        {
            CreateWall(pLength, pX, pY);
        }
    }

    private void PlayerCollision(List<Sprite> pWalls)
    {
        foreach (Sprite wall in pWalls)
        {
            if (_level.player.y > wall.y && wall.HitTest(_level.player))
            {
                _level.player.y += _scrollSpeed + 0.1f;
            }
            if (_level.player.y < wall.y && wall.HitTest(_level.player))
            {
                _level.player.y += _scrollSpeed;
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0; i < _length - 1; i++)
        {
            wallLength.RemoveAt(i);
        }
        _level.wallList.Remove(this);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Difficulty : GameObject
{
    const float ACCELERATION = 1f;
    const int MINUTES = 10; //1 minute

    private static float _scrollSpeed;
    private static int _tileSize;
    private static double _scoreModifier;

    public Difficulty()
    {
        _scrollSpeed = 1.0f;
        _tileSize = 64;
        _scoreModifier = 1;
    }

    private void Update()
    {
        DifficultySpeed();
        UpdateScoreModifier();
    }

    private void UpdateScoreModifier()
    {
        if (_scrollSpeed >= 1.0f && _scrollSpeed < 1.4f) _scoreModifier = 1;
        if (_scrollSpeed >= 1.4f && _scrollSpeed < 2.0f) _scoreModifier = 1.5;
        if (_scrollSpeed >= 2.0f) _scoreModifier = 2;
    }

    private void DifficultySpeed()
    {
        if (_scrollSpeed < 2.0f)
        {
            _scrollSpeed += ACCELERATION / game.currentFps / 50 / MINUTES;
        }
        else
        {
            _scrollSpeed = 2.0f;
        }
    }

    public static float GetScrollSpeed()
    {
        return _scrollSpeed;
    }
    public static int GetTileSize()
    {
        return _tileSize;
    }
    public static double GetScoreModifier()
    {
        return _scoreModifier;
    }
}

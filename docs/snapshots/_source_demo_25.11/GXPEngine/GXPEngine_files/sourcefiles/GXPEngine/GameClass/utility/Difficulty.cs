using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Difficulty : GameObject
{
    const float ACCELERATION = 1f;
    const int MINUTES = 10; //1 minute

    public static float oldScrollSpeed;
    public static float scrollSpeed;
    private static int _tileSize;
    private static double _scoreModifier;

    public Difficulty()
    {
        scrollSpeed = 1.0f;
        oldScrollSpeed = scrollSpeed;
        _tileSize = 64;
        _scoreModifier = 1;
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        DifficultySpeed();
        UpdateScoreModifier();
    }

    private void UpdateScoreModifier()
    {
        if (scrollSpeed >= 1.0f && scrollSpeed < 1.4f) _scoreModifier = 1;
        if (scrollSpeed >= 1.4f && scrollSpeed < 2.0f) _scoreModifier = 1.5;
        if (scrollSpeed >= 2.0f) _scoreModifier = 2;
    }

    private void DifficultySpeed()
    {
        if (scrollSpeed < 2.0f)
        {
            scrollSpeed += ACCELERATION / game.currentFps / 50 / MINUTES * Time.deltaTime;
        }
        else
        {
            scrollSpeed = 2.0f;
        }
    }
    public static void SetScrollSpeed(float newScrollSpeed)
    {
        oldScrollSpeed = scrollSpeed;
        scrollSpeed = newScrollSpeed;
    }
    public static float GetOldScrollSpeed()
    {
        return oldScrollSpeed;
    }
    public static float GetScrollSpeed()
    {
        return scrollSpeed;
    }
    public static int GetTileSize()
    {
        return _tileSize;
    }
    public static double GetScoreModifier()
    {
        return _scoreModifier;
    }
    public static void SetScoreModifier(double scoreMod)
    {
        _scoreModifier = scoreMod;
    }
}

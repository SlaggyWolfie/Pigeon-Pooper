using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class DeathManager : GameObject
{
    private bool _dead;
    private bool _onDeathPassed;

    private float _storeMusicVolume;
    private float _storeSFXVolume;

    private Level _level;
    private ScorePage _scorePage;


    public DeathManager(Level pLevel) : base()
    {
        _level = pLevel;
        _dead = false;
        _onDeathPassed = false;
    }

    private void Update()
    {
        DeathCheck();
        if (_dead) OnDeath();
        if (_onDeathPassed)
        {
            if (Input.GetKeyDown(Key.SPACE))
            {
                CloseEndScreen();
                NewMenu();
            }
        }
    }

    private void DeathCheck()
    {
        if (_level.player.energy <= 0)
        {
            _level.player.energy = 1;
            _dead = true;
        }
    }

    private void OnDeath()
    {
        PauseManager.Pause();
        SFX.PlaySound(SFX.death);
        _storeSFXVolume = SFX.sfxSoundChannel.Volume;
        SFX.SetSFXVolume(0);
        _storeMusicVolume = Music.musicVolume;
        Music.musicVolume = 0;

        ShowEndScreens(_level.player.score);
        _dead = false;
        _onDeathPassed = true;
    }

    private void ShowEndScreens(double score)
    {
        SFX.SetSFXVolume(_storeSFXVolume);
        _scorePage = new ScorePage(score);
        game.AddChild(_scorePage);

        Score scoreP = new Score();
        scoreP.SetScore("New Player".ToLower(), score);

        scoreP.Destroy();
        SFX.SetSFXVolume(0);
    }

    private void CloseEndScreen()
    {
        _scorePage.Destroy();
    }

    private void NewMenu()
    {
        Destroy();
        _level.Destroy();

        Menu menu = new Menu();
        game.AddChild(menu);

        PauseManager.UnPause();
        SFX.SetSFXVolume(_storeSFXVolume);
        Music.musicVolume = _storeMusicVolume;
    }
}
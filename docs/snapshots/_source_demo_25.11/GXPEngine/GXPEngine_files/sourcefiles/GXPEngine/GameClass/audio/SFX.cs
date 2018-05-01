using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class SFX
{
    private static float _sfxVolume = 1f;

    public static Sound poopSound = new Sound("assets/sounds/poop/poop_1.wav");
    public static Sound pickUpPowerUp = new Sound("assets/sounds/Power-up/power_up.wav");
    public static Sound losePowerUp = new Sound("assets/sounds/losing power-Up/losing power-up.wav");
    public static Sound losePowerUpAlt = new Sound("assets/sounds/losing power-Up/losing power-up_alt.wav");
    public static Sound debuff = new Sound("assets/sounds/Debuff/Debuff.wav");
    public static Sound enterSound = new Sound("assets/sounds/Enter/Enter.wav");
    public static Sound getHit = new Sound("assets/sounds/Hit/Hit_1.wav");
    public static Sound menuSwitching = new Sound("assets/sounds/Menu switch button/Menu switch button.wave");
    public static Sound death = new Sound("assets/sounds/Dead/Dead.wav");
    public static Sound killEnemy = new Sound("assets/sounds/Kill enemy/killenemy_1.wav");
    public static Sound killEnemyAlt = new Sound("assets/sounds/Kill enemy/killenemy_alt_1.wav");
    public static Sound applause1 = new Sound("assets/sounds/applause/Level1_1.mp3");
    public static Sound applause2 = new Sound("assets/sounds/applause/Level2_1.mp3");
    public static Sound applause3 = new Sound("assets/sounds/applause/Level3_1.mp3");
    public static Sound applause4 = new Sound("assets/sounds/applause/Level4_1.mp3");
    public static Sound applause5 = new Sound("assets/sounds/applause/Level5_1.mp3");
    public static Sound coin = new Sound("assets/sounds/coin.mp3");
    public static Sound explosion = new Sound("assets/sounds/explosion.mp3");

    private static List<Sound> sfxList = new List<Sound>
    {
        poopSound, pickUpPowerUp, losePowerUp, losePowerUpAlt,
        debuff, enterSound, getHit, menuSwitching, death,
        applause1, applause2, applause3, applause4, applause5,
        coin, explosion
    };

	public static SoundChannel sfxSoundChannel;

    /// <summary>
    /// Sets the sound. Input volume as 1..100.
    /// </summary>
    /// <param name="volume"></param>
    public static void SetSFXVolume(float volume)
    {
        _sfxVolume = volume / 100;
    }

    public static float GetSFXVolume()
    {
        return _sfxVolume;
    }

    public static void PlaySound(Sound sound)
    {
        sfxSoundChannel = sound.Play();
        sfxSoundChannel.Volume = _sfxVolume;
    }
}

/*
public class SFX
{
    public Sound poopSound;
    public Sound pickUpPowerUp;
    public Sound losePowerUp;
    public Sound debuff;
    public Sound enterSound;
    public Sound getHit;
    public Sound menuSwitching;
    public Sound death;
    public Sound applause1;
    public Sound applause2;
    public Sound applause3;
    public Sound applause4;
    public Sound applause5;
    public SFX()
    {
        poopSound = new Sound("assets/sounds/poop/poop_1.wav");
        pickUpPowerUp = new Sound("assets/sounds/Power-up/power_up.wav");
        losePowerUp = new Sound("assets/sounds/losingPowerUp/losing-power-up.wav");
        debuff = new Sound("assets/sounds/Debuff/Debuff.wav");
        enterSound = new Sound("assets/sounds/Enter/Enter.wav");
        getHit = new Sound("assets/sounds/Hit/Hit_1.wav");
        menuSwitching = new Sound("assets/sounds/Menu switch button/Menu switch button.wave");
        death = new Sound("assets/sounds/Dead/Dead.wav");
        applause1 = new Sound("assets/sounds/applause/Level1_1.mp3");
        applause2 = new Sound("assets/sounds/applause/Level2_1.mp3");
        applause3 = new Sound("assets/sounds/applause/Level3_1.mp3");
        applause4 = new Sound("assets/sounds/applause/Level4_1.mp3");
        applause5 = new Sound("assets/sounds/applause/Level5_1.mp3");
    }
} */

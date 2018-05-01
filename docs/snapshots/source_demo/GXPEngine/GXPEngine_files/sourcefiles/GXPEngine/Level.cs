using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Level : GameObject
{
    const float VICTIM_SCALE = 0.5f;

    private float _scrollSpeed;

    public Player player { get; private set; }

    //Optimised checking for collision
    public List<Victim> victimList = new List<Victim> { };
    public List<Projectile> poopList = new List<Projectile> { };

    public Level() : base()
    {
        _scrollSpeed = 0.5f;
        CreateVictim();
        CreatePlayer();
    }

    private void Update()
    {
        PoopControl();
    }

    private void CreatePlayer()
    {
        player = new Player();
        AddChild(player);
        player.SetXY(game.width / 2, game.height / 2);
    }

    private void CreateVictim()
    {
        Victim newVictim = new Victim(this);
        AddChild(newVictim);
        newVictim.SetScaleXY(VICTIM_SCALE, VICTIM_SCALE);
        victimList.Add(newVictim);
        if (victimList.Count <= 10)
        {
            Timer victimSpawner = new Timer(2000, CreateVictim);
        }
    }

    private void PoopControl()
    {
        if (Input.GetKeyDown(Key.SPACE))
        {
            Projectile newPoop = new Projectile(this);
            AddChild(newPoop);
            newPoop.SetXY(player.x, player.y);
            poopList.Add(newPoop);
        }
    }

    private void UpwardsScroll()
    {
        //Not used currently.
        y += _scrollSpeed;
    }
}

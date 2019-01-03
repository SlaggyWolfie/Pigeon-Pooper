using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class HUD : GameObject
{
    private Player _player;
    private TextBox _scoreboard;
    private TextBox _energyMeter;
    private TextBox _fpsCounter;

    public HUD(Player pPlayer)
    {
        _player = pPlayer;

        _scoreboard = new TextBox(game.width, game.height, game.width - 260, 20);
        AddChild(_scoreboard);

        _energyMeter = new TextBox(game.width, game.height, 0, 20);
        AddChild(_energyMeter);

        //Testing Purposes
        _fpsCounter = new TextBox(game.width, game.height, 20, game.height - 40);
        AddChild(_fpsCounter);
    }

    private void Update()
    {
        _scoreboard.SetText("score: ");
        _scoreboard.SetText("score: " + _player.score.ToString());

        _energyMeter.SetText("energy: ");
        _energyMeter.SetText("energy: " + _player.energy.ToString());

        _fpsCounter.SetText(game.currentFps.ToString());
    }
}

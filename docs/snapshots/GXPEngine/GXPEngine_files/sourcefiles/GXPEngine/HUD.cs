using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class HUD : GameObject
{
    private string _scoreText;
    private int _initialScoreLength;
    private float _scoreboardX;
    private float _scoreboardY;
    private float _energyX;
    private float _energyY;
    private float _fpsX;
    private float _fpsY;

    private Player _player;
    private TextBox _scoreboard;
    private TextBox _energyMeter;
    private TextBox _fpsCounter;

    public HUD(Player pPlayer)
    {
        //Getting the player
        _player = pPlayer;

        //Scoreboard Initialization
        _scoreboardX = game.width - 260;
        _scoreboardY = 20;
        _scoreboard = new TextBox(game.width, game.height, _scoreboardX, _scoreboardY);
        _scoreText = "score:";
        _initialScoreLength = _scoreText.Length;
        AddChild(_scoreboard);

        //Energy Meter initialization
        _energyX = 20;
        _energyY = 20;
        _energyMeter = new TextBox(game.width, game.height, _energyX, _energyY);
        AddChild(_energyMeter);

        //Testing Purposes
        _fpsX = 20;
        _fpsY = game.height - 40;
        _fpsCounter = new TextBox(game.width, game.height, _fpsX, _fpsY);
        AddChild(_fpsCounter);
    }

    private void Update()
    {
        _scoreText = "score: " + _player.score.ToString();
        //_scoreboard.SetText("score: ");
        //UpdateScoreboardPosition();
        _scoreboard.SetText(_scoreText);

        _energyMeter.SetText("energy: ");
        _energyMeter.SetText("energy: " + _player.energy.ToString());

        _fpsCounter.SetText(game.currentFps.ToString());
    }

    private void UpdateScoreboardPosition()
    {
        //Not used. Idk even.
        //initial X is game.width - 160
        if (_scoreboard != null)
        {
            _scoreboard.Destroy();
            _scoreboard = null;
        }
        //_scoreboard = new TextBox(game.width, game.height, _scoreboardX - (_scoreText.Length - _initialScoreLength) * 20, _scoreboardY);
    }
}

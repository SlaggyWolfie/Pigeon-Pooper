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
    private float _scoreboardX_offset;
    private float _energyX;
    private float _energyY;
    private float _fpsX;
    private float _fpsY;

    public static bool barOrMeter;

    private Sprite _energyBack;
    private Sprite _energyFront;
    private Player _player;
    private TextBox _scoreboard;
    private TextBox _energyMeter;
    private TextBox _fpsCounter;

    public HUD(Player pPlayer)
    {
        //Getting the player
        _player = pPlayer;

        //Scoreboard Initialization
        _scoreboardX = MyGame.OldX() - 160;
        _scoreboardX_offset = 25;
        _scoreboardY = 20;
        _scoreboard = new TextBox(game.width, game.height, _scoreboardX, _scoreboardY);
        _scoreText = "score:";
        _initialScoreLength = _scoreText.Length;
        AddChild(_scoreboard);

        //Energy Bar
        _energyBack = new Sprite("assets/hud/energy.png");
        AddChild(_energyBack);
        _energyBack.SetXY(-10, 10);
        _energyBack.scaleX = 2;
        _energyBack.scaleY = 0.5f;
        _energyFront = new Sprite("assets/hud/button_new.png");
        AddChild(_energyFront);
        _energyFront.SetXY(30, 19);
        _energyFront.scaleX = 3.3f;
        _energyFront.scaleY = 0.5f;
        _energyFront.SetColor(0, 1, 1);

        //Energy Meter initialization
        _energyX = 20;
        _energyY = 20;
        _energyMeter = new TextBox(game.width, game.height, _energyX, _energyY);
        AddChild(_energyMeter);

        //Testing Purposes
        _fpsX = 20;
        _fpsY = MyGame.OldY() - 40;
        _fpsCounter = new TextBox(game.width, game.height, _fpsX, _fpsY);
        AddChild(_fpsCounter);
        barOrMeter = true; // true for bar, false for meter
    }

    private void Update()
    {
        //Console.WriteLine(barOrMeter);
        if (PauseManager.GetPause()) return;
        _scoreText = "score: " + _player.score.ToString();
        _scoreboard.SetText(_scoreText, _scoreboardX - (_scoreText.Length - _initialScoreLength) * _scoreboardX_offset, _scoreboardY);

        BarOrMeter();

        _energyFront.scaleX = 3.3f * _player.energy / 100;
        if (_player.energy >= 100) _energyFront.scaleX = 3.3f;
        if (_energyFront.scaleX <= 0) _energyFront.alpha = 0;
        else _energyFront.alpha = 1;
        
        _energyMeter.SetText("energy: ");
        _energyMeter.SetText("energy: " + _player.energy.ToString());

        _fpsCounter.SetText(game.currentFps.ToString());
    }

    private void BarOrMeter()
    {
        if (barOrMeter)
        {
            _energyBack.visible = true;
            _energyFront.visible = true;
            _energyMeter.visible = false;
            _fpsCounter.visible = false;
        }

        if (!barOrMeter)
        {
            _energyBack.visible = false;
            _energyFront.visible = false;
            _energyMeter.visible = true;
            _fpsCounter.visible = true;
        }
    }
}

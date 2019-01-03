using System;
using GXPEngine;

public class Menu : GameObject
{

	Music menuMusic;// = new Music();
					//GXPEngine.SoundChannel.Volume(1);
	Button _button;
	QuitButton _quitbutton;
	ContButton _contbutton;
	ContPage _contpage;
	OptButton _optbutton;
	IntroButton _introbutton;
	Intro _intro;
	Vol0 _vol0;
	Vol20 _vol20;
	Vol40 _vol40;
	Vol60 _vol60;
	Vol80 _vol80;
	Vol100 _vol100;
	Menubg _menubg;
	InsertPage _insertpage;
	bool _ins = false;
	bool _selectedstart;
	bool _selectedexit;
	bool _selectedopti;
	bool _selectedctrl;
	bool _selectedintro;
	int state = 0;
	//public int _gamestarted = 0;
	int volstate = 6;
	int _contpagevis = 0;

	static bool _gamestarted = false;
		public static bool GetGameStarted()
		{
			return _gamestarted;
		}

    public Menu()
		{
            menuMusic = new Music();
            menuMusic.soundChannel = menuMusic.gameTrack.Play();

			_menubg = new Menubg();
			AddChild(_menubg);
			_menubg.x = game.width / 2;
			_menubg.y = game.height /2;
			_menubg.SetOrigin(_menubg.width / 2, _menubg.height / 2);
			_menubg.scaleX=(0.93f);
			_menubg.scaleY=(0.93f);

			_button = new Button();
			AddChild(_button);
			_button.SetOrigin(_button.width/2, _button.height/2);
			_button.y = (game.height - _button.height) / 4+ 150;
			_button.scaleX = (0.7f);
			_button.scaleY = (0.7f);

			_quitbutton = new QuitButton();
			AddChild(_quitbutton);
			_quitbutton.SetOrigin(_quitbutton.width / 2, _quitbutton.height / 2);
			_quitbutton.y = (game.height - _quitbutton.height) * 1.25f + 150;
			_quitbutton.scaleX = (0.7f);
			_quitbutton.scaleY = (0.7f);

			_contbutton = new ContButton();
			AddChild(_contbutton);
			_contbutton.SetOrigin(_contbutton.width / 2, _contbutton.height / 2);
			_contbutton.y = (game.height - _contbutton.height) / 2+ 150;
			_contbutton.scaleX = (0.7f);
			_contbutton.scaleY = (0.7f);

			_optbutton = new OptButton();
			AddChild(_optbutton);
			_optbutton.SetOrigin(_optbutton.width / 2, _optbutton.height / 2);
			_optbutton.y = (game.height - _optbutton.height) / 1.33f+ 150;
			_optbutton.scaleX = (0.7f);
			_optbutton.scaleY = (0.7f);

			_introbutton = new IntroButton();
			AddChild(_introbutton);
			_introbutton.SetOrigin(_introbutton.width / 2, _introbutton.height / 2);
			_introbutton.y = (game.height - _introbutton.height) +150;
			_introbutton.scaleX = (0.7f);
			_introbutton.scaleY = (0.7f);

			_contpage = new ContPage();
			AddChild(_contpage);
			_contpage.SetOrigin(_contpage.width / 2, _contpage.height / 2);
			_contpage.x = (game.width - _contpage.width) / 2;
			_contpage.y = (game.height - _contpage.height) / 2;
			_contpage.visible = false;

			_intro = new Intro();
			AddChild(_intro);
			_intro.width = game.width;
			_intro.height = game.height;

			_insertpage = new InsertPage();
			AddChild(_insertpage);
			_insertpage.width = game.width;
			_insertpage.height = game.height;

            //_intro.SetScaleXY(game.width / _intro.width, game.height / _intro.height);
            //_intro.SetScaleXY(0.93f, 0.93f);
			_intro.x = (game.width - _intro.width) / 2;
			_intro.y = (game.height - _intro.height) / 2;

			_vol0 = new Vol0();
			AddChild(_vol0);
			_vol0.visible = false;

			_vol20 = new Vol20();
			AddChild(_vol20);
			_vol20.visible = false;

			_vol40 = new Vol40();
			AddChild(_vol40);
			_vol40.visible = false;

			_vol60 = new Vol60();
			AddChild(_vol60);
			_vol60.visible = false;

			_vol80 = new Vol80();
			AddChild(_vol80);
			_vol80.visible = false;

			_vol100 = new Vol100();
			AddChild(_vol100);
			_vol100.visible = false;

		}

		void Update()
		{
			//check which button is 'active'
			if (state == 1)
			{ _selectedstart = true; }
			else
			{ _selectedstart = false; }

			if (state == 2)
			{ _selectedctrl = true; }
			else
			{ _selectedctrl = false; }

			if (state == 3)
			{ _selectedopti = true; }
			else
			{ _selectedopti = false; }

			if (state == 4)
			{ _selectedintro = true; }
			else
			{ _selectedintro = false; }

			if (state == 5)
			{ _selectedexit = true; }
			else
			{ _selectedexit = false; }


		//massive menu navigation section
		if (Input.GetKeyDown(Key.SPACE))
		{
			endintro();
			_ins = true;
			_insertpage.Destroy();
		}

			if (Input.GetKeyDown(Key.SPACE))
			{
				if (_selectedstart == true)
				{
					if (_gamestarted == false)
					{
                    SFX.PlaySound(SFX.enterSound);
                        loadGame();
						_selectedstart = false;
						_gamestarted = true;
					}


                }

				if (_selectedexit == true)
				{
                SFX.PlaySound(SFX.enterSound);
                    endGame();
				}

				if (_selectedctrl == true)
				{
                SFX.PlaySound(SFX.enterSound);
                    if (_contpagevis == 0)
					{
						_contpage.visible = true;
						_contpagevis = 1;
					}
					else
					{
						_contpage.visible = false;
						_contpagevis = 0;
					}
				}

				//volume slider space navigation
				if (_selectedopti == true)
				{
                SFX.PlaySound(SFX.enterSound);
                    if (_gamestarted == false)
					{
						if (volstate != 6)
						{
							volstate += 1;
						}

						if (volstate == 6)
						{
							volstate = 1;
						}
					}
				}

				//volume slider visibility aids section
				if (_selectedintro == true)
				{
                //SFX.PlaySound(SFX.enterSound);
                if (volstate == 1)
					{
						_vol0.visible = true;
						_vol20.visible = false;
						_vol40.visible = false;
						_vol60.visible = false;
						_vol80.visible = false;
						_vol100.visible = false;
					}
                // SHOW THE SCORE PAGE?

                if (_selectedintro == true)
                {
                    showintro();
                }
            }
			}

		//volume arrow key navigation
		if (_selectedopti == true)
		{
			if (Input.GetKeyDown(Key.LEFT) & volstate != 1)
			{
				SFX.enterSound.Play();
				volstate -= 1;
			}
			if (Input.GetKeyDown(Key.LEFT) & volstate == 1)
			{
				SFX.enterSound.Play();
				volstate = 6;
			}
			if (Input.GetKeyDown(Key.RIGHT) & volstate != 6)
			{
				SFX.enterSound.Play();
				volstate += 1;
			}
			if (Input.GetKeyDown(Key.RIGHT) & volstate == 6)
			{
				SFX.enterSound.Play();
				volstate = 1;
			}
		}
		if (_selectedopti == true)
		{
			//SFX.enterSound.Play();
			if (volstate == 1)
			{
				_vol0.visible = true;
				_vol20.visible = false;
				_vol40.visible = false;
				_vol60.visible = false;
				_vol80.visible = false;
				_vol100.visible = false;
				menuMusic.soundChannel.Volume = 0;
			}

			if (volstate == 2)
			{
				_vol0.visible = false;
				_vol20.visible = true;
				_vol40.visible = false;
				_vol60.visible = false;
				_vol80.visible = false;
				_vol100.visible = false;
				menuMusic.soundChannel.Volume = 0.2f;
			}

			if (volstate == 3)
			{
				_vol0.visible = false;
				_vol20.visible = false;
				_vol40.visible = true;
				_vol60.visible = false;
				_vol80.visible = false;
				_vol100.visible = false;
				menuMusic.soundChannel.Volume = 0.4f;
			}

			if (volstate == 4)
			{
				_vol0.visible = false;
				_vol20.visible = false;
				_vol40.visible = false;
				_vol60.visible = true;
				_vol80.visible = false;
				_vol100.visible = false;
				menuMusic.soundChannel.Volume = 0.6f;
			}

			if (volstate == 5)
			{
				_vol0.visible = false;
				_vol20.visible = false;
				_vol40.visible = false;
				_vol60.visible = false;
				_vol80.visible = true;
				_vol100.visible = false;
				menuMusic.soundChannel.Volume = 0.8f;
			}

			if (volstate == 6)
			{
				_vol0.visible = false;
				_vol20.visible = false;
				_vol40.visible = false;
				_vol60.visible = false;
				_vol80.visible = false;
				_vol100.visible = true;
				menuMusic.soundChannel.Volume = 1;

				
			}
            
        }
        //menu section over, vol slider visibility addition
        if (_selectedopti == false)
			{
				_vol0.visible = false;
				_vol20.visible = false;
				_vol40.visible = false;
				_vol60.visible = false;
				_vol80.visible = false;
				_vol100.visible = false;
			}

			if (Input.GetKeyDown(Key.ESCAPE))
			{
			if (_gamestarted == false)
				{
					endGame();
				}

			if (_gamestarted == true)
				{
					//HOW DO PAUSE
				}
			}
			//menu navigation
			if (Input.GetKeyDown(Key.DOWN) & state != 0)
			{
            SFX.PlaySound(SFX.menuSwitching);
			if (_gamestarted == false)
				{
					if (state < 5)
					{
						state += 1;
					}
				}
			}
			//more menu navigation
			if (Input.GetKeyDown(Key.UP) & state != 0)
			{
            SFX.PlaySound(SFX.menuSwitching);
			if (_gamestarted == false)
				{
					if (state > 1)
					{
						state -= 1;
					}
				}
			}

			//button highlight functions
			if (_selectedstart == true)
			{
				_button.x = MyGame.OldX() / 2+ 100;
			}
			else
			{
				_button.x = MyGame.OldX() / 2;
			}

			if (_selectedctrl == true)
			{
				_contbutton.x = MyGame.OldX() / 2+ 100;
			}
			else
			{
				_contbutton.x = MyGame.OldX() / 2;
			}

			if (_selectedopti == true)
			{
				_optbutton.x = MyGame.OldX() / 2+ 100;
			}
			else
			{
				_optbutton.x = MyGame.OldX() / 2;
			}

			if (_selectedexit == true)
			{
				_quitbutton.x = MyGame.OldX() / 2+ 100;
			}
			else
			{
				_quitbutton.x = MyGame.OldX() / 2;
			}

			if (_selectedintro == true)
			{
				_introbutton.x = MyGame.OldX() / 2+ 100;
			}
			else
			{
				_introbutton.x = MyGame.OldX() / 2;
			}
		}

		//menu functions
		void endintro()
		{
			if (state == 0 & _ins == true)
			{
				_intro.visible = false;
				state = 1;
			}
		}

	    void endGame()
		{
			game.Destroy();
		}

		void showintro()
        { 

        Score scoreList = new Score();
        AddChild(scoreList);
        scoreList.GetScore();
		}

		void loadGame()
		{
            menuMusic.soundChannel.Stop();
		    Level endless = new Level();
			game.AddChild(endless);
			_button.Destroy();
			_quitbutton.Destroy();
			_contpage.Destroy();
			_contbutton.Destroy();
			_optbutton.Destroy();
			_menubg.Destroy();
			_intro.Destroy();
			_introbutton.Destroy();
			_vol0.Destroy();
			_vol20.Destroy();
			_vol40.Destroy();
			_vol60.Destroy();
			_vol80.Destroy();
			_vol100.Destroy();
			_insertpage.Destroy();
		}

	}

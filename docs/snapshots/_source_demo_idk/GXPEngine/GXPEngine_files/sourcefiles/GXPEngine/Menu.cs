﻿using System;
namespace GXPEngine
{
	public class Menu : Sprite
	{
		Button _button;
		QuitButton _quitbutton;
		ContButton _contbutton;
		ContPage _contpage;
		OptButton _optbutton;
		IntroButton _introbutton;
		Intro _intro;
		Menubg _menubg;
		bool _selectedstart;
		bool _selectedexit;
		bool _selectedopti;
		bool _selectedctrl;
		bool _selectedintro;
		int _gamestarted = 0;
		int state = 0;
		int _contpagevis = 0;
        
		public Menu() : base("assets/menu/bgmenu.png")
		{
			_menubg = new Menubg();
			AddChild(_menubg);
			_menubg.x = (game.width - _menubg.width) / 2 + 100;
			_menubg.y = (game.height - _menubg.height) / 2 - 50;

			_intro = new Intro();
			AddChild(_intro);
			_intro.x = (game.width - _intro.width) / 2;
			_intro.y = (game.height - _intro.height) / 2-50;

			_contpage = new ContPage();
			AddChild(_contpage);
			_contpage.x = (game.width - _contpage.width) / 2 + 100;
			_contpage.y = (game.height - _contpage.height) / 2 - 50;
			_contpage.visible = false;

			_button = new Button();
			AddChild(_button);
			_button.y = (game.height - _button.height) / 64-50;

			_quitbutton = new QuitButton();
			AddChild(_quitbutton);
			_quitbutton.y = (game.height - _quitbutton.height) / 2 * 2-50;

			_contbutton = new ContButton();
			AddChild(_contbutton);
			_contbutton.y = (game.height - _contbutton.height) / 3-50;

			_optbutton = new OptButton();
			AddChild(_optbutton);
			_optbutton.y = (game.height - _optbutton.height) / 1.5f-50;

			_introbutton = new IntroButton();
			AddChild(_introbutton);
			_introbutton.y = (game.height - _introbutton.height) / 2 * 2.7f - 50;

		}

		void Update()
		{
			//check which button is 'active'
			if (state == 1)
			{ _selectedstart = true; }
			else
			{ _selectedstart = false; }

			if (state == 2)
				_selectedctrl = true;
			else
				_selectedctrl = false;

			if (state == 3)
			{ _selectedopti = true; }
			else
			{ _selectedopti = false; }

			if (state == 4)
			{ _selectedexit = true; }
			else
			{ _selectedexit = false; }

			if (state == 5)
			{ _selectedintro = true; }
			else
			{ _selectedintro = false; }


			//massive menu navigation section
			if (Input.GetKeyDown(Key.SPACE))
			{
				endintro();

				if (_selectedstart == true)
				{
					if (_gamestarted == 0)
					{
						loadGame();
						_selectedstart = false;
						_gamestarted = 1;
					}
				}

				if (_selectedexit == true)
				{
					endGame();
				}

				if (_selectedctrl == true)
				{
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

				if (_selectedopti == true)
				{
					// show a volume sprite and change with 20% steps?
				}

				if (_selectedintro == true)
				{
					showintro();
				}
			}

			if (Input.GetKeyDown(Key.ESCAPE))
			{
				if (_gamestarted == 0)
				{
					endGame();
				}

				if (_gamestarted == 1)
				{
					
				}
			}
			//menu navigation
			if (Input.GetKeyDown(Key.DOWN) & state != 0)
			{
				if (_gamestarted == 0)
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
				if (_gamestarted == 0)
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
				_button.x = (game.width - _button.width) / 2 + 200;
			}
			else
			{
				_button.x = (game.width - _button.width) / 2 + 100;
			}

			if (_selectedctrl == true)
			{
				_contbutton.x = (game.width - _button.width) / 2 + 200;
			}
			else
			{
				_contbutton.x = (game.width - _button.width) / 2 + 100;
			}

			if (_selectedopti == true)
			{
				_optbutton.x = (game.width - _optbutton.width) / 2 + 200;
			}
			else
			{
				_optbutton.x = (game.width - _optbutton.width) / 2 + 100;
			}

			if (_selectedexit == true)
			{
				_quitbutton.x = (game.width - _button.width) / 2 + 200;
			}
			else
			{
				_quitbutton.x = (game.width - _button.width) / 2 + 100;
			}

			if (_selectedintro == true)
			{
				_introbutton.x = (game.width - _button.width) / 2 + 200;
			}
			else
			{
				_introbutton.x = (game.width - _button.width) / 2 + 100;
			}
		}

		//menu functions
		void endintro()
		{
			if (state == 0)
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
			_intro.visible = true;
			state = 0;
		}

		void loadGame()
		{ 
		    Level endless = new Level();
			game.AddChild(endless);
			_button.Destroy();
			_quitbutton.Destroy();
			_contpage.Destroy();
			_contbutton.Destroy();
			_optbutton.Destroy();
			_menubg.Destroy();
		}
	}
}
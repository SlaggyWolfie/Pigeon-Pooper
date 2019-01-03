using System;
namespace GXPEngine
{
	public class Button : Sprite
	{
		public Button() : base("assets/menu/startbutton.png")
		{
		}
	}

	public class ContButton : Sprite
	{
		public ContButton() : base("assets/menu/contbutton.png")
		{
		}
	}

	public class Intro : Sprite
	{
		public Intro() : base("assets/menu/intropage.png")
		{
            SetScaleXY(0.85f, 0.85f);
		}
	}

	public class IntroButton : Sprite
	{
		public IntroButton() : base("assets/menu/introbutton.png")
		{
		}
	}

	public class QuitButton : Sprite
	{
		public QuitButton() : base("assets/menu/exitbutton.png")
		{
		}
	}

	public class ContPage : Sprite
	{
		public ContPage() : base("assets/menu/controls.png")
		{
		}
	}

	public class OptButton : Sprite
	{
		public OptButton() : base("assets/menu/optbutton.png")
		{
		}
	}

	public class Menubg : Sprite
	{
		public Menubg() : base("assets/menu/bgmenu1.png")
		{
		}
	}

	public class Vol0 : Sprite
	{
		public Vol0() : base("assets/hud/volume00.png")
		{
		}
	}

	public class Vol20 : Sprite
	{
		public Vol20() : base("assets/hud/volume20.png")
		{
		}
	}

	public class Vol40 : Sprite
	{
		public Vol40() : base("assets/hud/volume40.png")
		{
		}
	}

	public class Vol60 : Sprite
	{
		public Vol60() : base("assets/hud/volume60.png")
		{
		}
	}

	public class Vol80 : Sprite
	{
		public Vol80() : base("assets/hud/volume80.png")
		{
		}
	}

	public class Vol100 : Sprite
	{
		public Vol100() : base("assets/hud/volume100.png")
		{
		}
	}

	public class ScorePage : Sprite
	{
		public ScorePage() : base("assets/placeholders/circle.png")
		{
		}
	}

	public class DeathPage : Sprite
	{
		public DeathPage() : base("assets/placeholders/square.png")
		{
		}
	}

	public class InsertPage : Sprite
	{
		public InsertPage() : base("assets/menu/insert3.png")
		{
		}
	}
}

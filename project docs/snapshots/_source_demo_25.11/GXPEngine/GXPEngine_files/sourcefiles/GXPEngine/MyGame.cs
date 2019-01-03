using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{
    public Menu menu;
    public static float resolutionX;
    public static float resolutionY;

    //initialize game here
    public MyGame () : base(1024, 768, false)
	{
		menu = new Menu();
		AddChild(menu);

        resolutionX = game.width;
        resolutionY = game.height;
        
	}

	//update game here
	void Update ()
	{
        RescaleEverything();
        PauseControl();
	}

	//system starts here
	static void Main() 
	{
		new MyGame().Start();
	}

    public static float OldX()
    {
        return resolutionX;
    }
    public static float OldY()
    {
        return resolutionY;
    }

    private void Rescale(Sprite pSprite)
    {
        float xScale = (float)game.width / pSprite.width;
        float yScale = (float)game.height / pSprite.height;
        pSprite.SetScaleXY(xScale, yScale);
    }

    private void RescaleEverything()
    {
        //if (resolutionX != game.width || resolutionY != game.height)
        //{
            float xScale = game.width / resolutionX;
            float yScale = game.height / resolutionY;
            game.SetScaleXY(xScale, yScale);
            game.SetViewport(0, 0, game.width, game.height);
        //}
    }

    private void PauseControl()
    {
        if (Menu.GetGameStarted())
        {
            if (Input.GetKeyDown(Key.ESCAPE))
            {
                //Console.WriteLine(Menu.GetGameStarted());
                //Console.WriteLine(PauseManager.GetPause());

                if (PauseManager.GetPause() == true)
                {
                    PauseManager.UnPause();
                }
                else if (PauseManager.GetPause() == false)
                {
                    PauseManager.Pause();
                }
            }
        }
    }
}

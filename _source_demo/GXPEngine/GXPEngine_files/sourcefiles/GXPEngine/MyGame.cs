using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{
    public Menu menu;

    private ContPage pausePage;

    public static bool gameHasStarted;

    public static float resolutionX;
    public static float resolutionY;

    //initialize game here
    public MyGame () : base(1024, 768, false)
    {
        resolutionX = game.width;
        resolutionY = game.height;

        menu = new Menu();
		AddChild(menu);
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

    public static void LoadGame(GameObject someObject)
    {
        Level endless = new Level();
        someObject.AddChild(endless);
    }

    private void PauseControl()
    {
        if (gameHasStarted)
        {
            if (Input.GetKeyDown(Key.ESCAPE))
            {
                if (PauseManager.GetPause() == true)
                {
                    if (pausePage != null) pausePage.Destroy();
                    PauseManager.UnPause();
                }
                else if (PauseManager.GetPause() == false)
                {

                    pausePage = new ContPage();
                    AddChild(pausePage);
                    Rescale(pausePage);
                    PauseManager.Pause();
                }
                else if (PauseManager.GetPause() == true && Input.GetKeyDown(Key.M))
                {
                    //Wanted to return to the menu but eh...
                }
            }
        }
    }
}

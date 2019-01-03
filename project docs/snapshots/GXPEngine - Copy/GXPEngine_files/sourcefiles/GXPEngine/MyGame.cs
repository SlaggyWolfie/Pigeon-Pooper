using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{
    public static float resolutionX;
    public static float resolutionY;

    public enum DifficultyStages
    {
        STAGE1 = 10,
        STAGE2 = 12,
        STAGE3 = 14,
        STAGE4 = 16,
        STAGE5 = 20
    }

    //initialize game here
    public MyGame () : base(1024, 768, false)
	{
		Menu menu = new Menu();
		AddChild(menu);

        resolutionX = game.width;
        resolutionY = game.height;
        
	}

	//update game here
	void Update ()
	{

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
}

using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{
    private static float _scrollSpeed;
    private static int _tileSize;
    public static float resolutionX;
    public static float resolutionY;
    //initialize game here
    public MyGame () : base(1024, 768, false)
	{
        _scrollSpeed = 0.5f;
        _tileSize = 64;
        resolutionX = game.width;
        resolutionY = game.height;
        Level endless = new Level();
        AddChild(endless);
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

    public static float GetScrollSpeed()
    {
        return _scrollSpeed;
    }
    public static int GetTileSize()
    {
        return _tileSize;
    }
}

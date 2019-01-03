using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{	
	//initialize game here
	public MyGame () : base(800, 600, false)
	{
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
}

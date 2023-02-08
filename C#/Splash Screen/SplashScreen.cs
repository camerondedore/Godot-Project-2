using Godot;
using System;

public class SplashScreen : Node
{
	
	[Export]
	NodePath timerPath;
	[Export]
	string nextScenePath = "res://Scenes/MainMenu.tscn";

	Timer timer;



	public override void _Ready()
	{
		// get nodes
		timer = GetNode<Timer>(timerPath);

		// set up timer
		timer.Connect("timeout", this, "LoadNextScene");
	}



	void LoadNextScene()
	{
		// change scene
		GetTree().ChangeScene(nextScenePath);
	}
}

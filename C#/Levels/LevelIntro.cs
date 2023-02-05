using Godot;
using System;

public class LevelIntro : Node
{
	
	[Export]
	NodePath animationPlayerPath,
		levelNameLabelPath;
	[Export]
	Animation introAnimation;
	[Export]
	string levelName = "LEVEL NAME";

	AnimationPlayer animationPlayer;



	public override void _Ready()
	{
		// get nodes
		animationPlayer = GetNode<AnimationPlayer>(animationPlayerPath);
		var levelNameLabel = GetNode<Label>(levelNameLabelPath);

		// set level name label
		levelNameLabel.Text = levelName;

		// play animation
		animationPlayer.Play("LevelIntroAnimation");
	}



//  public override void _Process(float delta)
//  {
//      
//  }
}

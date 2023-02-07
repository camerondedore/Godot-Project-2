using Godot;
using System;

public class MainMenuUi : Node
{
	
	[Export]
	NodePath quitButtonPath,
		loginButtonPath,
		passwordEditPath,
		loginFailedLabelPath;
	[Export]
	string nextLevelPath = "res://Scenes/ChatScene1BookDepository";

	TextureButton quitButton;
	Button loginButton;
	LineEdit passwordEdit;
	Label loginFailedLabel;

	public override void _Ready()
	{
		// get nodes
		quitButton = GetNode<TextureButton>(quitButtonPath);
		loginButton = GetNode<Button>(loginButtonPath);
		passwordEdit = GetNode<LineEdit>(passwordEditPath);
		loginFailedLabel = GetNode<Label>(loginFailedLabelPath);

		// set up controls
		quitButton.Connect("pressed", this, "QuitButtonPressed");
		loginButton.Connect("pressed", this, "LoginButtonPressed");
		
		passwordEdit.GrabFocus();
		loginFailedLabel.Visible = false;
	}



	public override void _Process(float delta)
	{
		// check for enter key
		if(Input.GetActionStrength("menu-send") != 0)
		{
			// login
			LoginButtonPressed();
		}
	}



	private void QuitButtonPressed()
	{
		// quit game
		GetTree().Quit();
	}



	private void LoginButtonPressed()
	{
		if(passwordEdit.Text.Length > 0)
		{
			loginFailedLabel.Visible = false;
			
			// change scene
			GetTree().ChangeScene(nextLevelPath);
		}
		else
		{
			loginFailedLabel.Visible = true;
		}
	}
}

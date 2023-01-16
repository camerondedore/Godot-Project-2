using Godot;
using System;

public class PauseMenu : Node
{
   
    [Export]
    NodePath menuPath,
        resumeButtonPath,
        restartButtonPath,
        quitButtonPath;
    Control menu;
    Button restartButton,
        quitButton;
    TextureButton resumeButton;



    public override void _Ready()
    {
        // get nodes
        menu = GetNode<Control>(menuPath);
        resumeButton = GetNode<TextureButton>(resumeButtonPath);
        restartButton = GetNode<Button>(restartButtonPath);
        quitButton = GetNode<Button>(quitButtonPath);

        // set up buttons
        resumeButton.Connect("pressed", this, "Resume");
        restartButton.Connect("pressed", this, "Restart");
        quitButton.Connect("pressed", this, "Quit");
    }



    public override void _Process(float delta)
    {
        if(menu.Visible == false && Engine.TimeScale == 0)
        {
            // enable menu
            menu.Visible = true;
        }

        if(menu.Visible == true && Engine.TimeScale != 0)
        {
            // disable menu
            menu.Visible = false;
        }
    }



    void Resume()
    {
        Pause.ResumeGame();
    }



    void Quit()
    {
        GetTree().Quit();
    }



    void Restart()
    {
        // var currentScene = this.Owner.Filename;
        GetTree().ChangeScene(this.Owner.Filename);
        
        // incomplete, for dev only
        // GetTree().ReloadCurrentScene();
        // RequestReady();
    }



    // void SetTime(float value)
    // {
    //     Pause.savedTimeScale = value;
    // }
}

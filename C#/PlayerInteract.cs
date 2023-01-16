using Godot;
using System;

public class PlayerInteract : RayCast
{
    
    [Export]
    NodePath itemLabelPath,
        letterTimerPath;

    Label itemLabel;
    Timer letterTimer;



    public override void _Ready()
    {
        // get nodes
        itemLabel = GetNode<Label>(itemLabelPath);
        letterTimer = GetNode<Timer>(letterTimerPath);

        // set up timer
        letterTimer.Connect("timeout", this, "AddLetter");

        // clear label
        itemLabel.Text = "";
    }



    void AddLetter()
    {
        itemLabel.VisibleCharacters++;
    }



    public override void _PhysicsProcess(float delta)
    {
        if(Engine.TimeScale == 0)
        {
            return;
        }

        if(IsColliding())
        {
            var hitObject = (Spatial) GetCollider();
            
            // item in ray, set label to item name
            if(itemLabel.Text != hitObject.Name)
            {
                itemLabel.Text = hitObject.Name;
                itemLabel.VisibleCharacters = 0;
                itemLabel.PercentVisible = 0;
                letterTimer.Start();
            }
        }
        else
        {
            // no item in ray, clear text
            if(itemLabel.Text != "")
            {
                itemLabel.Text = "";
                itemLabel.VisibleCharacters = 0;
                itemLabel.PercentVisible = 0;
                letterTimer.Stop();
            }            
        }
    }
}

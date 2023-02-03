using Godot;
using System;

public class TaskList : Node
{
    
    [Export]
    NodePath taskListOpenPath;

    Control taskListOpen;
    Disconnector discon = new Disconnector();


    
    public override void _Ready()
    {
        // get nodes
        taskListOpen = GetNode<Control>(taskListOpenPath);
    }



    public override void _Process(float delta)
    {
        if(discon.Trip(PlayerInput.objective))
        {
            taskListOpen.Visible = !taskListOpen.Visible;
        }
    }
}

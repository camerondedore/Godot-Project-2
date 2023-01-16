using Godot;
using System;

public class TestInteractableProp : Spatial, IInteractable
{
    [Export]
    string propName = "prop";

    

    public string GetInteractableName()
    {
        return propName;        
    }



    public void Interact(Node actor)
    {
        this.QueueFree();
    }
}

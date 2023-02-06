using Godot;
using System;

public class PlaceableProp : Spatial, IInteractable
{
    [Export]
    string propName = "prop";
    [Export]
    int taskIndex = 0;
    [Export]
    NodePath taskListPath,
        placeholderModelPath,
        placedModelPath;

    public bool active = true;

    TaskList taskList;
    Spatial placeholderModel,
        placedModel;



    public override void _Ready()
    {
        // get nodes
        taskList = GetNode<TaskList>(taskListPath);
        placeholderModel = GetNode<Spatial>(placeholderModelPath);
        placedModel = GetNode<Spatial>(placedModelPath);
    }

    

    public string GetInteractableName()
    {
        return propName;        
    }



    public void Interact(Node actor)
    {        
        placeholderModel.Visible = false;
        placedModel.Visible = true;
        active = false;

        taskList.CompleteTask(taskIndex);
    }



    public bool CanInteract()
    {
        return active;
    }
}

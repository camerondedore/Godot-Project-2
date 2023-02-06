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
        placedModelPath,
        placeAudioPath;
    [Export]
    AudioStream placedSound;

    public bool active = true;

    TaskList taskList;
    Spatial placeholderModel,
        placedModel;
    AudioTools placeAudio;



    public override void _Ready()
    {
        // get nodes
        taskList = GetNode<TaskList>(taskListPath);
        placeholderModel = GetNode<Spatial>(placeholderModelPath);
        placedModel = GetNode<Spatial>(placedModelPath);
        placeAudio = GetNode<AudioTools>(placeAudioPath);
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

        placeAudio.PlaySound(this, placedSound);
    }



    public bool CanInteract()
    {
        return active;
    }
}

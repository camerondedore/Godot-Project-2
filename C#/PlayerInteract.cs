using Godot;
using System;

public class PlayerInteract : RayCast
{
    
    [Export]
    NodePath itemLabelPath,
        letterTimerPath,
        letterAudioPath;

    [Export]
    AudioStream[] letterSounds;
    [Export]
    AudioStream lettersCompleteSound;

    Label itemLabel;
    Timer letterTimer;
    AudioTools letterAudio;
    Disconnector discon = new Disconnector();
    IInteractable activeItem = null;



    public override void _Ready()
    {
        // get nodes
        itemLabel = GetNode<Label>(itemLabelPath);
        letterTimer = GetNode<Timer>(letterTimerPath);
        letterAudio = GetNode<AudioTools>(letterAudioPath);

        // set up timer
        letterTimer.Connect("timeout", this, "AddLetter");

        // clear label
        itemLabel.Text = "";
    }



    void AddLetter()
    {
        if(itemLabel.VisibleCharacters >= itemLabel.Text.Length)
        {
            // done typing
            letterTimer.Stop();
            letterAudio.PlaySound((Node) this, lettersCompleteSound);
            return;
        }

        itemLabel.VisibleCharacters++;
        letterAudio.PlayRandomSound((Node) this, letterSounds);
    }



    public override void _Process(float delta)
    {
        if(discon.Trip(PlayerInput.interact))
        {
            // interact with item
            activeItem.Interact(this);
        }
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

            if(hitObject is IInteractable)
            {
                activeItem = (IInteractable) hitObject;

                // interactable item in ray, set label to item name
                if(itemLabel.Text != activeItem.GetInteractableName())
                {
                    itemLabel.Text = activeItem.GetInteractableName();
                    itemLabel.VisibleCharacters = 0;
                    itemLabel.PercentVisible = 0;
                    letterTimer.Start();

                    return;
                }

                if(itemLabel.Text == activeItem.GetInteractableName())
                {
                    // interactable item in ray, label is already item name
                    return;
                }   
            }
        }
    
        // no interactable item in ray
        activeItem = null;

        // clear text
        if(itemLabel.Text != "")
        {
            itemLabel.Text = "";
            itemLabel.VisibleCharacters = 0;
            itemLabel.PercentVisible = 0;
            letterTimer.Stop();
        }
    }
}

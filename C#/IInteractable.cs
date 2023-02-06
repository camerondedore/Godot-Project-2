using Godot;
using System;

public interface IInteractable
{
    string GetInteractableName();
    void Interact(Node actor);
    bool CanInteract();
}

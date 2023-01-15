using Godot;
using System;

public class CharacterUi : Node
{
    
    [Export]
    NodePath uiRootPath;
    bool showUi = true;
    Control uiRoot;




    public override void _Ready()
    {       
        // get nodes
        uiRoot = GetNode<Control>(uiRootPath);
    }



    public override void _Process(float delta)
    {
         if(uiRoot.Visible == true && Engine.TimeScale == 0)
        {
            // disable ui
            uiRoot.Visible = false;            
        }

        if(uiRoot.Visible == false && Engine.TimeScale != 0)
        {
            if(showUi)
            {
                // enable ui
                uiRoot.Visible = true;
            }
        }
    }



    public void ShowUi()
    {
        uiRoot.Visible = true;
        showUi = true;
    }



    public void HideUi()
    {
        uiRoot.Visible = false;
        showUi = false;
    }
}

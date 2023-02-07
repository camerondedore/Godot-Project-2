using Godot;
using System;

public class SettingsUi : Node
{
    // PLACE THIS NODE BELOW SETTINGS NODE
    
    [Export]
    NodePath mouseSpinBoxPath,
        bloomCheckBoxPath,
        ssaoCheckBoxPath;

    SpinBox mouseSpinBox;
    CheckBox bloomCheckBox,
        ssaoCheckBox;



    public override void _Ready()
    {
        // get nodes
        mouseSpinBox = GetNode<SpinBox>(mouseSpinBoxPath);
        bloomCheckBox = GetNode<CheckBox>(bloomCheckBoxPath);
        ssaoCheckBox = GetNode<CheckBox>(ssaoCheckBoxPath);

        // set up controls
        mouseSpinBox.Value = Settings.currentSettings.mouse;
        bloomCheckBox.Pressed = Settings.currentSettings.bloom;
        ssaoCheckBox.Pressed = Settings.currentSettings.ssao;
        
        mouseSpinBox.Connect("value_changed", this, "MouseSpinnerChanged");
        bloomCheckBox.Connect("toggled", this, "BloomCheckBoxChanged");
        ssaoCheckBox.Connect("toggled", this, "SsaoCheckBoxChanged");
    }



    void MouseSpinnerChanged(float value)
    {
        Settings.currentSettings.mouse = value;
        Settings.SaveSettings();
    }



    void BloomCheckBoxChanged(bool pressed)
    {
        Settings.currentSettings.bloom = pressed;
        Settings.SaveSettings();
    }



    void SsaoCheckBoxChanged(bool pressed)
    {
        Settings.currentSettings.ssao = pressed;
        Settings.SaveSettings();
    }
}

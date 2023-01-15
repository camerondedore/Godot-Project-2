using Godot;
using System;

public class CameraControllerSimple : Spatial
{
    




    public override void _Ready()
    {
        
    }



    public override void _PhysicsProcess(float delta)
    {
        // update camera to match this node
        GlobalCamera.camera.LookAtFromPosition(GlobalTransform.origin, GlobalTransform.origin + GlobalTransform.basis.z * -10, Vector3.Up);
    }
}

using Godot;
using System;

public class CameraControllerSimple : Spatial
{
    
    [Export]
	float sensitivity = 0.15f,
		minAngle = -50,
		maxAngle = 40;



    public override void _Ready()
    {
        // lock cursor
		Input.SetMouseMode(Input.MouseMode.Captured);
    }



    public override void _PhysicsProcess(float delta)
    {
        // update camera to match this node
        GlobalCamera.camera.LookAtFromPosition(GlobalTransform.origin, GlobalTransform.origin + GlobalTransform.basis.z * -10, Vector3.Up);
    }



    public override void _UnhandledInput(InputEvent e)
	{	
		if(Engine.TimeScale == 0)
		{
			return;
		}

		if(e is InputEventMouseMotion)
		{
			var mouseDirection = RotationDegrees;
			mouseDirection.x -= PlayerInput.look.y * sensitivity;
			mouseDirection.x = Mathf.Clamp(mouseDirection.x, minAngle, maxAngle);
			mouseDirection.y -= PlayerInput.look.x * sensitivity;
			mouseDirection.y = Mathf.Wrap(mouseDirection.y, 0, 360);

			RotationDegrees = mouseDirection;
		}
	}
}

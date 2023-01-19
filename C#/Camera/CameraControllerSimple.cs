using Godot;
using System;

public class CameraControllerSimple : Spatial
{
    
	[Export]
	NodePath characterNodePath;
    [Export]
	float sensitivity = 0.15f,
		minAngle = -50,
		maxAngle = 40,
		fovSpeed = 20;

	Character character;
	float normalFov,
		targetFov;



    public override void _Ready()
    {
        // lock cursor
		Input.SetMouseMode(Input.MouseMode.Captured);

		// get nodes
		character = GetNode<Character>(characterNodePath);

		// get fov
		normalFov = GlobalCamera.camera.Fov;
		targetFov = normalFov;
    }



    public override void _PhysicsProcess(float delta)
    {
        // update camera to match this node
        GlobalCamera.camera.LookAtFromPosition(GlobalTransform.origin, GlobalTransform.origin + GlobalTransform.basis.z * -10, Vector3.Up);

		// update camera fov
		targetFov = Mathf.Lerp(targetFov, normalFov, fovSpeed * delta);
		GlobalCamera.camera.Fov = Mathf.Lerp(GlobalCamera.camera.Fov, targetFov, fovSpeed * delta);
    }



    public override void _UnhandledInput(InputEvent e)
	{	
		if(Engine.TimeScale == 0)
		{
			return;
		}

		if(e is InputEventMouseMotion)
		{
			var headRotation = RotationDegrees;
			var bodyRotation = character.RotationDegrees;

			headRotation.x -= PlayerInput.look.y * sensitivity;
			headRotation.x = Mathf.Clamp(headRotation.x, minAngle, maxAngle);
			bodyRotation.y -= PlayerInput.look.x * sensitivity;
			bodyRotation.y = Mathf.Wrap(bodyRotation.y, 0, 360);

			// apply rotation to head and body
			RotationDegrees = headRotation;
			character.RotationDegrees = bodyRotation;
		}
	}



	public void SetTargetFov(float fov)
	{
		targetFov = fov;
	}
}

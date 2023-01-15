using Godot;
using System;

public class CharacterStateLand : CharacterState
{

    float startTime;



	public override void RunState(float delta)
	{
		// set snap to grab floor
		blackboard.snap = -blackboard.GetFloorNormal();

		
		// get input
		var moveDirection = Vector3.Zero;
		moveDirection.x = PlayerInput.move.x;
		moveDirection.z = PlayerInput.move.z;

		// use camera space
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.cameraSpringArm.Rotation.y).Normalized();


		// set up velocity using input
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration);


		// apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;


		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlideWithSnap(blackboard.velocity, blackboard.snap, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);


		// get camera look vector
		var cameraForward = -blackboard.cameraSpringArm.GlobalTransform.basis.z;
		cameraForward.y = 0;
		
		// get camera look position
		var lookPosition = cameraForward + blackboard.GlobalTransform.origin;
		
		// apply look
		blackboard.LookAt(lookPosition, Vector3.Up);
		

		// camera follow
		blackboard.cameraSpringArm.MoveToFollowCharacter(blackboard.GlobalTransform.origin, blackboard.velocity);
	}



	public override void StartState()
	{
        startTime = EngineTime.timePassed;
	}



	public override void EndState()
	{
		// set start altitude low to avoid scatter shot
		blackboard.jumpStartY = -10000;
		blackboard.fallStartY = -10000;
	}



	public override State Transition()
	{
		if(EngineTime.timePassed > startTime + blackboard.landTime)
		{
			// move
			return blackboard.stateIdle;
		}

		return this;
	}
}

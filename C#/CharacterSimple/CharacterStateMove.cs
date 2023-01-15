using Godot;
using System;

public class CharacterStateMove : CharacterState
{





	public override void RunState(float delta)
	{
		// set snap to grab floor
		blackboard.snap = -blackboard.GetFloorNormal();

		
		// get input
		var moveDirection = Vector3.Zero;
		moveDirection.x = PlayerInput.move.x;
		moveDirection.z = PlayerInput.move.z;

		// use camera space
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.cameraController.Rotation.y).Normalized();


		// set up velocity using input
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration);


		// apply gravity into floor
		blackboard.velocity += blackboard.gravity * blackboard.GetFloorNormal() * delta;


		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlideWithSnap(blackboard.velocity, blackboard.snap, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);


		// get camera look vector
		var cameraForward = -blackboard.cameraController.GlobalTransform.basis.z;
		cameraForward.y = 0;
		
		// get camera look position
		var lookPosition = cameraForward + blackboard.GlobalTransform.origin;
		
		// apply look
		blackboard.LookAt(lookPosition, Vector3.Up);
	}



	public override void StartState()
	{

	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		if(!blackboard.IsOnFloor())
		{
			// get start altitude
			blackboard.fallStartY = blackboard.GlobalTransform.origin.y;

			// fall
			return blackboard.stateFall;
		}

		if(blackboard.IsOnFloor() && blackboard.jumpDisconnector.Trip(PlayerInput.jump))
		{
			// jump start
			return blackboard.stateJumpStart;
		}

		if(!PlayerInput.isMoving)
		{
			// move
			return blackboard.stateIdle;
		}

		return this;
	}
}

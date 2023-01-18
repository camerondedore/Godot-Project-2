using Godot;
using System;

public class CharacterStateMove : CharacterState
{

	float stepDistance = 0;



	public override void RunState(float delta)
	{
		// set snap to grab floor
		blackboard.snap = -blackboard.GetFloorNormal();

		
		// get input
		var moveDirection = Vector3.Zero;
		moveDirection.x = PlayerInput.move.x;
		moveDirection.z = PlayerInput.move.z;

		// convert move direction to local space
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.Rotation.y).Normalized();


		// set up velocity using input
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration);


		// apply gravity into floor
		blackboard.velocity += blackboard.gravity * blackboard.GetFloorNormal() * delta;


		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlideWithSnap(blackboard.velocity, blackboard.snap, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);


		// add speed to step distance
		stepDistance += blackboard.velocity.Length() * delta;

		// check for stride
		if(stepDistance > blackboard.stepDistance)
		{
			stepDistance = 0;
			blackboard.feetAudio.PlayRandomSound((Node) blackboard, blackboard.footstepSounds);
		}
	}



	public override void StartState()
	{
		stepDistance = 0;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		if(!blackboard.IsOnFloor())
		{
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

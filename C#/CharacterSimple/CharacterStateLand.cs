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

		// convert move direction to local space
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.Rotation.y).Normalized();


		// set up velocity using input
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration);


		// apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;


		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlideWithSnap(blackboard.velocity, blackboard.snap, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);
	}



	public override void StartState()
	{
        startTime = EngineTime.timePassed;

		// land audio
		blackboard.feetAudio.PlaySound((Node) blackboard, blackboard.landSound);
	}



	public override void EndState()
	{

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

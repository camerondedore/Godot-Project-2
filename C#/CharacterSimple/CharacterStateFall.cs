using Godot;
using System;

public class CharacterStateFall : CharacterState
{





	public override void RunState(float delta)
	{
		// get input
		var moveDirection = Vector3.Zero;
		moveDirection.x = PlayerInput.move.x;
		moveDirection.z = PlayerInput.move.z;

		// convert move direction to local space
		moveDirection = moveDirection.Rotated(Vector3.Up, blackboard.Rotation.y).Normalized();


		// set up velocity using persistent y
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, moveDirection.x * blackboard.speed, delta * blackboard.acceleration * 0.3f);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, moveDirection.z * blackboard.speed, delta * blackboard.acceleration * 0.3f);
		

		// apply gravity using persistent y
		blackboard.ySpeed += -blackboard.gravityMagnitude * delta;
		blackboard.velocity.y = blackboard.ySpeed;


		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlide(blackboard.velocity, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);
	}



	public override void StartState()
	{
		// set y to match previous velocity.y
		blackboard.ySpeed = blackboard.velocity.y;
	}



	public override void EndState()
	{
		// set y to match previous velocity.y
		blackboard.ySpeed = blackboard.velocity.y;
	}



	public override State Transition()
	{
		if(blackboard.IsOnFloor())
		{
			// land
			return blackboard.stateLand;
		}

		return this;
	}
}

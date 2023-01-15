using Godot;
using System;

public class CharacterStateJump : CharacterState
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


		// apply gravity
		blackboard.velocity.y += blackboard.gravity * delta;

		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlide(blackboard.velocity, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);
	}



	public override void StartState()
	{
		// set vertical speed; v = (-2hg)>(1/2)
		blackboard.velocity.y = Mathf.Sqrt((-2 * blackboard.jumpHeight * blackboard.gravity));
		
		// set snap to zero to release from floor
		blackboard.snap = Vector3.Zero;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		if(blackboard.velocity.y <= 0 && !blackboard.IsOnFloor())
        {
            // fall
            return blackboard.stateFall;
        }

		if(blackboard.IsOnFloor())
		{
			// move
			return blackboard.stateMove;
		}

		return this;
	}
}
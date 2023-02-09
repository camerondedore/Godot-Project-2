using Godot;
using System;

public class CharacterStateIdle : CharacterState
{





    public override void RunState(float delta)
	{		
        // set velocity
		blackboard.velocity.x = Mathf.Lerp(blackboard.velocity.x, 0, delta * blackboard.acceleration);
		blackboard.velocity.z = Mathf.Lerp(blackboard.velocity.z, 0, delta * blackboard.acceleration);

		// apply gravity into floor
		//blackboard.velocity += blackboard.gravityMagnitude * blackboard.gravityUnitVector * delta;
		
		
		// apply velocity
		blackboard.velocity = blackboard.MoveAndSlideWithSnap(blackboard.velocity, blackboard.snap, Vector3.Up, true, 4, blackboard.maxSlopeAngleRad);
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
			// fall
			return blackboard.stateFall;
		}

        if(blackboard.jumpDisconnector.Trip(PlayerInput.jump) && blackboard.IsOnFloor())
        {
            // jump start
            return blackboard.stateJumpStart;
        }
        
        if(PlayerInput.isMoving)
        {
            // move
            return blackboard.stateMove;
        }

		return this;
	}
}

using Godot;
using System;

public class PlayerInput : Node
{
	// Player Input is static for global availability

	public static Vector3 move;
	public static bool isMoving;
	public static Vector2 look;
	public static float pause,
		jump,
		fire1,
		fire2;
	public static bool isMouseMoving;


	public override void _Ready()
	{
		
	}



	public override void _Process(float delta)
	{
		// get move input
		// y will be skipped to make movement easier to apply
		move = Vector3.Zero;
		move.x = Input.GetActionStrength("player-right") - Input.GetActionStrength("player-left");
		move.z = Input.GetActionStrength("player-backward") - Input.GetActionStrength("player-forward");   

		// to determine if move input is greater than 0 faster
		if(move.x != 0 || move.z != 0)
		{
			isMoving = true;
		}
		else
		{
			isMoving = false;
		}


		// get button inputs
		pause = Input.GetActionStrength("player-pause");
		jump = Input.GetActionStrength("player-jump");
		fire1 = Input.GetActionStrength("player-fire-1");
		fire2 = Input.GetActionStrength("player-fire-2");

		// set mouse moving to false, this will be reset by the unhandled input method
		isMouseMoving = false;
	}



	public override void _UnhandledInput(InputEvent e)
	{	
		// get look input
		if(e is InputEventMouseMotion)
		{
			look.x = ((InputEventMouseMotion) e).Relative.x;
			look.y = ((InputEventMouseMotion) e).Relative.y;

			isMouseMoving = true;
		}
	}
}

using Godot;
using System;

public class GlobalCamera : Camera
{
	
	public static GlobalCamera camera;



	public override void _Ready()
	{
		// set static reference
		camera = this;
	}
}

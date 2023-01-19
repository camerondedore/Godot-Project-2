using Godot;
using System;

public class GlobalCamera : Camera
{
	/// PLACE THE CAMERA NODE ABOVE OTHER NODES THAT REFERENCE THIS IN _READY	
	public static GlobalCamera camera;



	public override void _Ready()
	{
		// set static reference
		camera = this;
	}
}

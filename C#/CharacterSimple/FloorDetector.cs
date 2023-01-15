using Godot;
using System;

public class FloorDetector : Node
{
	[Export]
	public NodePath characterPath;
	Character blackboard;



	public override void _Ready()
	{
		// get character blackboard
		blackboard = GetNode<Character>(characterPath);
	}



	public override void _PhysicsProcess(float delta)
	{
		// check for jump pad
		if(blackboard.GetSlideCount() > 0)
		{
			var collisionCount = blackboard.GetSlideCount();

			while(collisionCount > 0)
			{
				collisionCount--;
				var node = (Node) blackboard.GetSlideCollision(collisionCount).Collider;
				var nodeGroups = node.GetGroups();
			}
		}
	}
}

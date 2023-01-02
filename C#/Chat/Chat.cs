using Godot;
using System;

public class Chat : Node
{
	
	public StateMachine machine = new StateMachine(); 
	public State stateStart;

	[Export]
	NodePath chatUiPath;

	[Export]
	public string startChatFileLocalDirectory = "/c#/chat/chat-level-1-start.txt";

	public string gameDirectory;
	public ChatUi chatUi;


	

	public override void _Ready()
	{
		gameDirectory = System.IO.Directory.GetCurrentDirectory();

		// get nodes
		chatUi = GetNode<ChatUi>(chatUiPath);

		// initialize states
		stateStart = new ChatStateStart(){blackboard = this};

		// set first state in machine
		machine.SetState(stateStart);
	}



	public override void _Process(float delta)
	{	
		// run machine
		if(machine != null && machine.CurrentState != null)
		{
			machine.CurrentState.RunState(delta);
			machine.SetState(machine.CurrentState.Transition());
		}
	}
}

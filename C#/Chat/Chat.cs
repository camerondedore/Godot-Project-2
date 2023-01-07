using Godot;
using System;
using System.Collections.Generic;

public class Chat : Node
{
	
	public StateMachine machine = new StateMachine(); 
	public State stateStart,
		stateIdle,
		stateRespond;

	[Export]
	NodePath chatUiPath;

	[Export]
	public string startChatFileLocalDirectory = "/c#/chat/chat-level-1-start.txt",
		responseChatFileLocalDirectory = "/c#/chat/chat-level-1-responses.txt";

	public string gameDirectory;
	public ChatUi chatUi;
	public List<ChatUi.ChatLine> chatStartLines = new List<ChatUi.ChatLine>(), 
		chatResponseLines = new List<ChatUi.ChatLine>();


	

	public override void _Ready()
	{
		gameDirectory = System.IO.Directory.GetCurrentDirectory();

		// get nodes
		chatUi = GetNode<ChatUi>(chatUiPath);

		// initialize states
		stateStart = new ChatStateStart(){blackboard = this};
		stateIdle = new ChatStateIdle(){blackboard = this};
		stateRespond = new ChatStateRespond(){blackboard = this};

		// set up chat start
		// get chat from file
        var chatLinesStartRaw = System.IO.File.ReadAllText(gameDirectory + startChatFileLocalDirectory).Split('\n');
       
        // convert chat lines from raw into objects
        foreach(var chatLineStartRaw in chatLinesStartRaw)
        {
            chatStartLines.Add(new ChatUi.ChatLine(chatLineStartRaw));
        }

		// set up chat responses
		// get chat from file
		var chatLinesResponsesRaw = System.IO.File.ReadAllText(gameDirectory + responseChatFileLocalDirectory).Split('\n');

		// convert chat lines from raw into objects
		foreach(var chatLineReponseRaw in chatLinesResponsesRaw)
		{
			chatResponseLines.Add(new ChatUi.ChatLine(chatLineReponseRaw));
		}

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

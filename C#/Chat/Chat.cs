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
	public ChatLinesResource startChatResource,
		responseChatResource;

	public string gameDirectory;
	public ChatUi chatUi;
	public List<ChatLine> chatStartLines = new List<ChatLine>(), 
		chatResponseLines = new List<ChatLine>();


	

	public override void _Ready()
	{
		//gameDirectory = System.IO.Directory.GetCurrentDirectory();

		// get nodes
		chatUi = GetNode<ChatUi>(chatUiPath);

		// initialize states
		stateStart = new ChatStateStart(){blackboard = this};
		stateIdle = new ChatStateIdle(){blackboard = this};
		stateRespond = new ChatStateRespond(){blackboard = this};

		// set up chat start
        // convert chat lines from raw into objects
        foreach(var chatLineStartRaw in startChatResource.ChatLines)
        {
            chatStartLines.Add(new ChatLine(chatLineStartRaw));
        }

		// set up chat responses
		// convert chat lines from raw into objects
		foreach(var chatLineReponseRaw in responseChatResource.ChatLines)
		{
			chatResponseLines.Add(new ChatLine(chatLineReponseRaw));
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



	public class ChatLine
	{
		/*
			CHAT START
			Format: <time float>:<action string>:<user string>:<color string hexadecimal>:<message string>
			Example: "2;message;user1;#9911ff;what's up?"
			actions: logon, message, logoff

			CHAT RESPONSES
			Format: <placeholder float>:<keyword string>:<user string>:<color string hexadecimal>:<message string>
			Example: "0;target;user1;#9911ff;the target is a dissident"
			"gibberish" is the only keyword reserved by the chat behavior to respond to messages with no keyword
		*/
		
		public float time;
		public string action,
			user,
			color, 
			message;
		

		
		public ChatLine(string rawMessage)
		{
			// split raw message and assign fields
			string[] splitMessage = rawMessage.Split(';');
			
			time = float.Parse(splitMessage[0]);
			action = splitMessage[1];
			user = splitMessage[2];
			color = splitMessage[3];

			if(splitMessage.Length >= 5)
			{
				message = splitMessage[4];
			}
		}
	}
}

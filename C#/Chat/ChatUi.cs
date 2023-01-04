using Godot;
using System;

public class ChatUi : Node
{
	
	[Export]
	public string chatRoomName = "#FaeRequestChat",
		playerName = "ruby",
		playerColor = "#ff7700";

	[Export]
	NodePath userMessagePath,
		chatHistoryPath;

	public string newMessage;
	RichTextLabel chatHistory;
	LineEdit userMessage;



	public override void _Ready()
	{
		// get ui controls
		userMessage = GetNode<LineEdit>(userMessagePath);
		chatHistory = GetNode<RichTextLabel>(chatHistoryPath);

		// set user message box as focused
		userMessage.GrabFocus();
	}



	public override void _Process(float delta)
	{
		// check for enter key
		if(Input.GetActionStrength("chat_send_message") != 0)
		{
			// send message
			_on_SendButton_pressed();
		}
	}



	private void _on_SendButton_pressed()
	{
		// check for text
		if(userMessage.Text.Length == 0)
		{
			// no text
			return;
		}

		AddMessage(playerName, playerColor, userMessage.Text);

		newMessage = userMessage.Text;

		userMessage.Clear();
	}



	private void _on_QuitButton_pressed()
	{
		// quit game
		GetTree().Quit();
	}



	public void AddMessage(string user, string color, string message)
	{
		// HH:MM:SS    <user 8 char> | <message 100 char>
		var userCapped = $"        {user}";
		userCapped = userCapped.Substring(userCapped.Length - 8, 8); // get characters on right side

		var newMessage = $"\n{GetTime()}    [color={color}]{userCapped}[/color] | {message}";

		chatHistory.AppendBbcode(newMessage);		
	}



	public void LogOnUser(string user, string color)
	{
		// HH:MM:SS         --> | <message 100 char>

		var newMessage = $"\n{GetTime()}         [color=green]-->[/color] | [color={color}]{user}[/color] [color=green]has joined[/color] {chatRoomName}";

		chatHistory.AppendBbcode(newMessage);
	}



	public void LogOffUser(string user, string color)
	{
		// HH:MM:SS         <-- | <message 100 char>

		var newMessage = $"\n{GetTime()}         [color=red]<--[/color] | [color={color}]{user}[/color] [color=red]has left[/color] (Client quit)";

		chatHistory.AppendBbcode(newMessage);
	}



	string GetTime()
	{
		var time = OS.GetTime();

		var hour = time["hour"];
		hour = (int) hour < 10 ? $"0{hour}" : hour; // force to 2 digits

		var minute = time["minute"];
		minute = (int) minute < 10 ? $"0{minute}" : minute; // force to 2 digits

		var second = time["second"];
		second = (int) second < 10 ? $"0{second}" : second; // force to 2 digits

		return $"{hour}:{minute}:{second}";
	}



	public class ChatLine
	{
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

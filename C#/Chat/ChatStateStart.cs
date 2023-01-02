using Godot;
using System;
using System.Collections.Generic;

public class ChatStateStart : ChatState
{

    string[] chatLinesRaw;
    List<ChatUi.ChatLine> chatLines = new List<ChatUi.ChatLine>();
    int chatIndex = 0;
    float startTime = 0;



    public override void RunState(float delta)
    {
        if(chatLines.Count == 0)
        {
            // no more chat lines
            return;
        }

        var chatLine = chatLines[0];

        if(startTime + chatLine.time < EngineTime.timePassed)
        {
            if(chatLine.action == "logon")
            {
                // logon
                blackboard.chatUi.LogOnUser(chatLine.user, chatLine.color);
            }
            else if(chatLine.action == "message")
            {
                // send chat
                blackboard.chatUi.AddMessage(chatLine.user, chatLine.color, chatLine.message);
            }
            else if(chatLine.action == "logoff")
            {
                // logoff
                blackboard.chatUi.LogOffUser(chatLine.user, chatLine.color);
            }
            

            // remove sent chat from list
            chatLines.Remove(chatLine);
        }
    }



    public override void StartState()
    {
        startTime = EngineTime.timePassed;

        // get chat from file
        chatLinesRaw = System.IO.File.ReadAllText(blackboard.gameDirectory + blackboard.startChatFileLocalDirectory).Split('\n');
       
        // convert chat lines from raw into objects
        foreach(var chatLineRaw in chatLinesRaw)
        {
            chatLines.Add(new ChatUi.ChatLine(chatLineRaw));
        }
    }



    public override void EndState()
    {
        
    }



    public override State Transition()
    {
        return this;
    }
}

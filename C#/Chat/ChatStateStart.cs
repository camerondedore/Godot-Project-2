using Godot;
using System;
using System.Collections.Generic;

public class ChatStateStart : ChatState
{

    List<ChatUi.ChatLine> chatLines = new List<ChatUi.ChatLine>();
    float startTime = 0;



    public override void RunState(float delta)
    {
        if(chatLines.Count == 0)
        {
            // no more chat lines
            return;
        }

        var chatLine = chatLines[0];

        if(EngineTime.timePassed > startTime + chatLine.time)
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
        var chatLinesRaw = System.IO.File.ReadAllText(blackboard.gameDirectory + blackboard.startChatFileLocalDirectory).Split('\n');
       
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
        // check if start chat lines are finished
        if(chatLines.Count == 0 && EngineTime.timePassed > startTime + 2)
        {
            // idle
            return blackboard.stateIdle;
        }

        return this;
    }
}

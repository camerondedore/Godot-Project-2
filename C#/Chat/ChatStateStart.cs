using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ChatStateStart : ChatState
{

    float startTime = 0;



    public override void RunState(float delta)
    {
        if(blackboard.chatStartLines.Count == 0)
        {
            // no more chat lines
            return;
        }

        var chatLine = blackboard.chatStartLines[0];

        if(EngineTime.timePassed > startTime + chatLine.time)
        {
            if(chatLine.action == "logon")
            {
                // logon
                blackboard.chatUi.LogonUser(chatLine.user, chatLine.color);
            }
            else if(chatLine.action == "message")
            {
                // send chat
                blackboard.chatUi.AddMessage(chatLine.user, chatLine.color, chatLine.message, false);
            }
            else if(chatLine.action == "logoff")
            {
                // logoff
                blackboard.chatUi.LogoffUser(chatLine.user, chatLine.color);
            }
            

            // remove sent chat from list
            blackboard.chatStartLines.Remove(chatLine);
        }
    }



    public override void StartState()
    {
        startTime = EngineTime.timePassed;

        // set keywords label
        blackboard.chatUi.SetKeywords(blackboard.chatResponseLines.Where(c => c.time > -1).Select(c => c.action).ToArray());
    }



    public override void EndState()
    {
        
    }



    public override State Transition()
    {
        // check if start chat lines are finished
        if(blackboard.chatStartLines.Count == 0 && EngineTime.timePassed > startTime + 2)
        {
            // idle
            return blackboard.stateIdle;
        }

        return this;
    }
}

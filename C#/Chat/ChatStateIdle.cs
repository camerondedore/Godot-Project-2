using Godot;
using System;
using System.Collections.Generic;

public class ChatStateIdle : ChatState
{





    public override void RunState(float delta)
    {

    }



    public override void StartState()
    {
       
    }



    public override void EndState()
    {
        
    }



    public override State Transition()
    {
        // check for new message
        if(blackboard.chatUi.newMessage != "")
        {
            // respond
            return blackboard.stateRespond;
        }

        return this;
    }
}

using Godot;
using System;

public class ChatStateRespond : ChatState
{

    float startTime;
    bool responded;



    public override void RunState(float delta)
    {
        if(!responded && EngineTime.timePassed > startTime + 1f)
        {
            // temporary reply
            blackboard.chatUi.AddMessage("colfax", "#9911ff","you're dumb");

            responded = true;
        }
    }



    public override void StartState()
    {
        startTime = EngineTime.timePassed;
        responded = false;
    }



    public override void EndState()
    {
        // clear new message
        blackboard.chatUi.newMessage = "";
    }



    public override State Transition()
    {
        // check for reply
        if(responded)
        {
            // idle
            return blackboard.stateIdle;
        }

        return this;
    }
}

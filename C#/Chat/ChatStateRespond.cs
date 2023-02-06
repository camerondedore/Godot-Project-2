using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ChatStateRespond : ChatState
{

    
    float startTime;
    bool responded;



    public override void RunState(float delta)
    {
        if(!responded && EngineTime.timePassed > startTime + 1f)
        {
            var newMessageWords = blackboard.chatUi.newUserMessage.Split(' ');

            // clear new message
            blackboard.chatUi.newUserMessage = "";

            // check for new message words in chat lines
            var chatLineMatch = blackboard.chatResponseLines.Where(c => newMessageWords.Contains(c.action) && c.time > -1).FirstOrDefault();

            if(chatLineMatch != null)
            {
                blackboard.chatUi.AddMessage(chatLineMatch.user, chatLineMatch.color, chatLineMatch.message, false);
            }
            else
            {
                // get gibberish chat line
                var chatLineMatchGibberish = blackboard.chatResponseLines.Where(c => c.time == -1).First();
                
                blackboard.chatUi.AddMessage(chatLineMatchGibberish.user, chatLineMatchGibberish.color, chatLineMatchGibberish.message, false);
            }
            

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

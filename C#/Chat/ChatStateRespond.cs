using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ChatStateRespond : ChatState
{

    List<ChatUi.ChatLine> chatLines = new List<ChatUi.ChatLine>();
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
            var chatLineMatch = chatLines.Where(c => newMessageWords.Contains(c.action) && c.time > -1).FirstOrDefault();

            if(chatLineMatch != null)
            {
                blackboard.chatUi.AddMessage(chatLineMatch.user, chatLineMatch.color, chatLineMatch.message);
            }
            else
            {
                // get gibberish chat line
                var chatLineMatchGibberish = chatLines.Where(c => c.time == -1).First();
                
                blackboard.chatUi.AddMessage(chatLineMatchGibberish.user, chatLineMatchGibberish.color, chatLineMatchGibberish.message);
            }
            

            responded = true;
        }
    }



    public override void StartState()
    {
        startTime = EngineTime.timePassed;
        responded = false;

        if(chatLines.Count == 0)
        {
            // get chat from file
            var chatLinesRaw = System.IO.File.ReadAllText(blackboard.gameDirectory + blackboard.responseChatFileLocalDirectory).Split('\n');

            // convert chat lines from raw into objects
            foreach(var chatLineRaw in chatLinesRaw)
            {
                chatLines.Add(new ChatUi.ChatLine(chatLineRaw));
            }
        }
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

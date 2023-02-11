using Godot;
using System;

public class ChatLinesResource : Resource
{

    [Export]
    public string[] ChatLines {get; set;}



    public ChatLinesResource()
    {
        ChatLines = null;
    }



    public ChatLinesResource(string[] cl = null)
    {
        ChatLines = cl;
    }
}

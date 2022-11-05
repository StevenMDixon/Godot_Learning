using Godot;
using System;

public class World : Node2D
{
    private Global global;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
        if(!global.doorName.Empty())
        {
            Node doorNode = FindNode(global.doorName);
            if(doorNode != null)
            {
                Player player = GetNode<Player>("YSort/Player");
                player.Position =  new Vector2(344,70);
            }
        } 
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

using Godot;
using System;

public class CaveEntrance : Area2D
{
    [Export(PropertyHint.File, "*.tscn,*.scn")]
    String newScene;

    private Global global;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
    }


    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent.IsActionPressed("ui_accept"))
        {
            if(GetOverlappingBodies().Count > 0 )
            {
                NextLevel();
            }
        }
    }

    public void NextLevel()
    {
        GetTree().ChangeScene(newScene);
        global.doorName = Name; 
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

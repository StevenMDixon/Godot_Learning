using Godot;
using System;

public class CaveExit : Area2D
{
    [Export(PropertyHint.File, "*.tscn,*.scn")]
     String newScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
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
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

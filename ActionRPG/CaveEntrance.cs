using Godot;
using System;
using System.Linq;

public class CaveEntrance : Area2D
{
    [Export(PropertyHint.File, "*.tscn,*.scn")]
    String newScene;

    private Global global;
    private SceneTransition sceneTransition;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
        sceneTransition = GetNode<SceneTransition>("/root/SceneTransition");
    }


    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent.IsActionPressed("ui_accept"))
        {

            Godot.Collections.Array overlapping = GetOverlappingBodies();
            foreach(Node item in overlapping)
            {
                if(item.IsInGroup("Player")){
                    GD.Print(GetOverlappingBodies());
                    NextLevel();
                }
            }
        }
    }

    public void NextLevel()
    {
        //GetTree().ChangeScene(newScene);
        sceneTransition.ChangeScene(newScene);
        global.doorName = Name; 
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

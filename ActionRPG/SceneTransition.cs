using Godot;
using System;

public class SceneTransition : CanvasLayer
{
    private AnimationPlayer anim;
    private string targetScene = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        anim = GetNode<AnimationPlayer>("Test");
    }

    public void ChangeScene(String scene)
    {
        targetScene = scene;
        anim.Play("Dissolve");
    }

    public void _on_Test_animation_finished(string anim_name)
    {
        if(anim_name == "Dissolve" && targetScene != null)
        {
            GetTree().ChangeScene(targetScene);
            targetScene = null;
            anim.PlayBackwards("Dissolve");
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

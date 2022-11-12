using Godot;
using System;

public class DashGhost : AnimatedSprite
{
    Tween tween;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tween = GetNode<Tween>("Tween");
        tween.InterpolateProperty(this, "modulate:a", 1.0, 0.0, 0.2f);
        tween.Start();
    }

    public void setStats(float direction){
        if(direction < 0){
            this.FlipH = true;
        } else {
            this.FlipH = false;
        }
    }

    public void _on_Tween_tween_completed(Godot.Object obj, NodePath key){
        QueueFree();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

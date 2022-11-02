using Godot;
using System;

public class Obstacle : Node2D
{
    [Signal] public delegate void score();
    private const int SPEED = 215;

    private AudioStreamPlayer2D point;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        point = GetNode<AudioStreamPlayer2D>("Point");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public override void _PhysicsProcess(float delta)
    {
        Position = new Vector2(Position.x -SPEED * delta, Position.y);
        if(GlobalPosition.x <= -200)
        {
            QueueFree();
        }
    }

    public void _on_Wall_body_entered(Node2D body)
    {
        if(body.Name == "Player")
        {
            Player player = (Player)body;
            player.die();
        }
    }

    public void _on_ScoreArea_body_exited(Node2D body)
    {
        if(body.Name == "Player")
        {
            Player player = (Player)body;
            if(player.Alive)
            {
            point.Play();
            EmitSignal("score");
            }
        }
    }

}

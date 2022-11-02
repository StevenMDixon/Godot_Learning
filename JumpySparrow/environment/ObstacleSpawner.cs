using Godot;
using System;

public class ObstacleSpawner : Node2D
{
    [Signal] public delegate void ObstacleCreated(Obstacle obstacle);
    private Timer timer;
    private PackedScene Obstacle;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        Obstacle = (PackedScene)ResourceLoader.Load("res://Environment/Obstacle.tscn");
        GD.Randomize();
    }

    public void _on_Timer_timeout()
    {
        spawnObstacle();
    }

    public void spawnObstacle()
    {
        Node2D obstacle = (Node2D)Obstacle.Instance();
        AddChild(obstacle);
        obstacle.Position = new Vector2(0, GD.Randi() % 100 + 300);
        EmitSignal("ObstacleCreated", obstacle);
    }

    public void start()
    {
        timer.Start();
    }
    
    public void stop()
    {
        timer.Stop();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

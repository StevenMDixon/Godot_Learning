using Godot;
using System;

public class Slime : KinematicBody2D
{
    public int speed = 25;
    public int health = 10;

    public Vector2 motion = new Vector2(0,0);

    private KinematicBody2D chaseTarget = null;

    private Particles2D trail;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        trail = GetNode<Particles2D>("Particles2D");
    }

    public override void _PhysicsProcess(float delta)
    {
        motion = Vector2.Zero;
        if(chaseTarget != null){
            motion = Position.DirectionTo(chaseTarget.Position) * speed;
            trail.Emitting = true;
            GD.Print(motion.x);
            if(motion.x > 0) trail.RotationDegrees = -180;
            else if (motion.x < 0) trail.RotationDegrees = 0;
        } else {
            trail.Emitting = false;
        }
        motion = MoveAndSlide(motion);
    }

    public void _on_DetectionArea_body_entered(KinematicBody2D body)
    {
        if(body.Name == "Player")
        {
            chaseTarget = body;
        }
    }

    public void _on_DetectionArea_body_exited(KinematicBody2D body)
    {
        if(body.Name == "Player")
        {
            chaseTarget = null;
        }
    }

    public void handleDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            QueueFree();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

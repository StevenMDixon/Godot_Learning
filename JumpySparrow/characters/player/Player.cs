using Godot;
using System;
public class Player : RigidBody2D
{
    [Signal] public delegate void Died();
    private bool started = false;
    private AnimationPlayer playerAnimations;

    private AudioStreamPlayer2D wing;
    private AudioStreamPlayer2D hit;
    private const float MAX_ROTATION_DEG = -30.0f;
    [Export] private int Flap_Force = 200;

    public bool Alive = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        wing = GetNode<AudioStreamPlayer2D>("Wing");
        hit = GetNode<AudioStreamPlayer2D>("Hit");
        playerAnimations = GetNode<AnimationPlayer>("AnimationPlayer");
        playerAnimations.Play("Idle");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _PhysicsProcess(float delta)
    {
       if(Input.IsActionJustPressed("flap") && Alive)
       {
           if(!started)
                start();
            flap();
       }

       if(RotationDegrees <= MAX_ROTATION_DEG)
       {
           AngularVelocity = 0;
           RotationDegrees = MAX_ROTATION_DEG;
       }

       if(LinearVelocity.y > 0)
       {
           if(RotationDegrees <= 90)
           {
               AngularVelocity = 1;
           }
           else
           {
               AngularVelocity = 0;
           }
       } 
    }

    public void start()
    {
        if(started) return;
        started = true;
        GravityScale = 5.0f;
        playerAnimations.Play("Flap");
    }

    public void flap()
    {
        LinearVelocity = new Vector2(0, -Flap_Force);
        AngularVelocity = -1;
        wing.Play();
    }

    public void die()
    {
        if(!Alive) return;
        Alive = false;
        playerAnimations.Stop();
        hit.Play();
        EmitSignal("Died");
    }
}

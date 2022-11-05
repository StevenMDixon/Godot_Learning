using Godot;
using System;

public class Player : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private AnimatedSprite animationPlayer;

    private Attack attack;
    private const int ACELLERATION = 25;
    private int MAX_SPEED = 50;
    private const int FRICTION = 80;

    private Vector2 motion = new Vector2();

    private bool isRunning;
    private const int WALK_SPEED = 50;
    private const int RUN_SPEED = 80;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimatedSprite>("AnimatedSprite");
        attack = GetNode<Attack>("Attack");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
       if(@event.IsActionPressed("Attack"))
       {
           attack.doAttack();
       }
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 input = new Vector2();
        input.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        input.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        input = input.Normalized();



        if(Input.IsActionJustPressed("Run"))
        {
            isRunning = true;
        }
        if(Input.IsActionJustReleased("Run"))
        {
            isRunning = false;
        }

        if(input != Vector2.Zero)
        {
            if(isRunning)
            {
                animationPlayer.Play("Run");
            } 
            else
            {
                animationPlayer.Play("Walk");
            }

            if(input.x > 0)
            {
                animationPlayer.FlipH = false;
            } 
            else
            {
                animationPlayer.FlipH = true;
            }

            if(isRunning)
            {
                MAX_SPEED = RUN_SPEED;
            } 
            else 
            {
                MAX_SPEED = WALK_SPEED;
            }

            motion += input * ACELLERATION * delta;
            motion = motion.LimitLength(MAX_SPEED * delta);
        }
        else 
        {
            motion = motion.MoveToward(Vector2.Zero, FRICTION * delta);
            animationPlayer.Play("Idle");
        }

        MoveAndCollide(motion);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

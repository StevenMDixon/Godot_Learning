using Godot;
using System;
using System.Collections.Generic;

public class Slime : KinematicBody2D
{
    public int speed = 25;
    public int health = 10;
    
    public enum State
    {
        IDLE, 
        NEW_DIR,
        MOVE, 
        FOLLOWING_PLAYER
    }

    public State currentState = State.IDLE;

    public Vector2 currentDirection = Vector2.Left;

    private List<Vector2> possibleDirections = new List<Vector2>{Vector2.Left, Vector2.Right, Vector2.Up, Vector2.Down};

    public Vector2 motion = new Vector2(0,0);

    private KinematicBody2D chaseTarget = null;

    private Particles2D trail;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        trail = GetNode<Particles2D>("Particles2D");
        GD.Randomize();
    }

    public override void _Process(float delta)
    {
        switch(currentState)
        {
            case State.IDLE:
                break;
            case State.NEW_DIR:
                currentDirection = chooseDir(possibleDirections);
                break;
            case State.MOVE:
                break;
            case State.FOLLOWING_PLAYER:
                break;
        }
    }

    public Vector2 chooseDir(List<Vector2> directions){
        int choice = (int)GD.RandRange(0, directions.Count);
        return directions[choice];
    }

    public State chooseNewState(List<State> states){
        int choice = (int)GD.RandRange(0, states.Count);
        return states[choice];
    }

    public override void _PhysicsProcess(float delta)
    {
        motion = Vector2.Zero;

        if(currentState == State.MOVE || currentState == State.FOLLOWING_PLAYER){
            if(chaseTarget != null && currentState == State.FOLLOWING_PLAYER)
            {
                motion = Position.DirectionTo(chaseTarget.Position) * speed;
            } else if(currentState == State.MOVE)
            {
                motion += (currentDirection * speed);
            }
        
            trail.Emitting = true;
           
            if(motion.x > 0) trail.RotationDegrees = -180;
            else if (motion.x < 0) trail.RotationDegrees = 0;

            motion = MoveAndSlide(motion);
        }   
        else
        {
            trail.Emitting = false;
        }
    }

    public void _on_DetectionArea_body_entered(KinematicBody2D body)
    {
        if(body.Name == "Player")
        {
            chaseTarget = body;
            currentState = State.FOLLOWING_PLAYER;
        }
    }

    public void _on_DetectionArea_body_exited(KinematicBody2D body)
    {
        if(body.Name == "Player")
        {
            chaseTarget = null;
            currentState = State.IDLE;
        }
    }

    public void handleDamage(int damage)
    {
        GD.Print("Ouch");
        health -= damage;
        if(health <= 0)
        {
            QueueFree();
        }
    }

    public void _on_Timer_timeout()
    {
        if(currentState != State.FOLLOWING_PLAYER)
        {
            currentState = chooseNewState(new List<State>{State.IDLE, State.MOVE, State.NEW_DIR});
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

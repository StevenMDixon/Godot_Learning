using Godot;
using System;

public class Attack : Area2D
{
    private AnimationPlayer animationPlayer;

    [Export] public int damage = 10;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = GetParent().GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void doAttack()
    {
        animationPlayer.Play("Attack");
    }

    public void _on_Attack_body_entered(Node2D body){
        if(body.Name == "Slime") {
            Slime slime = (Slime)body;
            slime.handleDamage(damage);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

using Godot;
using System;

public class HUD : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Label score;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        score = (Label)GetNode("Score");
    }

    public void updateScore(int newScore)
    {
        score.Text = newScore.ToString();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

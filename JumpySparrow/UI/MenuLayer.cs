using Godot;
using System;

public class MenuLayer : CanvasLayer
{
    [Signal] public delegate void StartGame();
    private TextureRect startMessage;
    private Tween tween;
    private Label scoreLabel;
    private Label highScoreLabel;
    private Control gameOverMenu;
    public bool gameStarted = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        startMessage = GetNode<TextureRect>("StartMenu/StartMessage");
        tween = GetNode<Tween>("Tween");
        scoreLabel = GetNode<Label>("GameOverMenu/VBoxContainer/ScoreLabel");
        highScoreLabel = GetNode<Label>("GameOverMenu/VBoxContainer/HighScoreLabel");
        gameOverMenu = GetNode<Control>("GameOverMenu");
    }

    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent.IsActionPressed("flap") && !gameStarted)
        {
            EmitSignal("StartGame");
            tween.InterpolateProperty(startMessage, "modulate:a", 1, 0, 0.5f);
            tween.Start();
            gameStarted = true;
        }
    }

    public void gameOverMenuStart(int score, int highScore)
    {
        scoreLabel.Text = "Score: " + score.ToString();
        highScoreLabel.Text = "Best: " +highScore.ToString();
        gameOverMenu.Visible = true;
    }

    public void _on_RestartButton_pressed(){
        GetTree().ReloadCurrentScene();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

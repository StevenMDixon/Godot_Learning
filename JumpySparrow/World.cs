using Godot;
using System;

public class World : Node2D
{
    private HUD hud;
    private ObstacleSpawner obstacleSpawner;
    private Node2D ground;
    private MenuLayer menuLayer;
    private int score = 0;
    private int highScore = 0;
    private const string SaveFilePath = "user://savedata.save";
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        menuLayer = GetNode<MenuLayer>("MenuLayer");
        hud = GetNode<HUD>("HUD");
        obstacleSpawner = GetNode<ObstacleSpawner>("ObstacleSpawner");
        ground = GetNode<Node2D>("Ground");
        obstacleSpawner.Connect("ObstacleCreated", this, "onObstacleCreated");
        //newGame();
    }

    public void newGame()
    {
        score = 0;
        hud.Visible = true;
        obstacleSpawner.start();
    }

    public void setScore(int value)
    {
        score = value;
        hud.updateScore(value);
    }

    public void playerScore(){
        setScore(score + 1);
    }

    public void gameOver()
    {
        obstacleSpawner.stop();
        ground.GetNode<AnimationPlayer>("AnimationPlayer").Stop();
        GetTree().CallGroup("obstacles", "set_physics_process", false);
        loadHighScore();
        if(score > highScore) 
        {
            highScore = score;
            saveHighScore();
        }
        menuLayer.gameOverMenuStart(score, highScore);
    }

    public void saveHighScore()
    {
        File saveFile = new File();
        var err = saveFile.Open(SaveFilePath, File.ModeFlags.Write);
        if(err == 0)
        {
            saveFile.StoreVar(highScore);
            saveFile.Close();
        }
    }

    public void loadHighScore()
    {
        File saveFile = new File();
        if(saveFile.FileExists(SaveFilePath))
        {
            var err = saveFile.Open(SaveFilePath, File.ModeFlags.Read);
            if(err == 0)
            {
                highScore = (int)saveFile.GetVar();
                saveFile.Close();
            }
        }
    }

    // Signal Responses
    public void onObstacleCreated(Obstacle obstacle){
        obstacle.Connect("score", this, "playerScore");
    }
    
    public void _on_DeathZone_body_entered(Node2D body){
        if(body.Name == "Player")
        {
            Player player = (Player)body;
            player.die();
        }
    }

    public void _on_Player_Died()
    {
        gameOver();
    }

    public void _on_MenuLayer_StartGame()
    {
        newGame();
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

using Godot;
using System;
using System.Linq;

public class GUI : CanvasLayer
{
    Label HealthCounter;
    TextureProgress HealthBar;
    Label ScoreCounter;
    Label EnemyCounter;
    int maxHealth;
    public int score = 0;

    public override void _Ready()
    {
        HealthCounter = GetNode<Label>("MarginContainer/HBoxContainer/Bars/LifeBar/TextureProgress/Number");
        HealthBar = GetNode<TextureProgress>("MarginContainer/HBoxContainer/Bars/LifeBar/TextureProgress");
        ScoreCounter = GetNode<Label>("MarginContainer/HBoxContainer/Counters/ScoreCounter/Background/Number");
        EnemyCounter = GetNode<Label>("MarginContainer/HBoxContainer/Counters/EnemyCounter/Background/Number");
        SetMaxHealthCounter(100);
    }

    public void SetMaxHealthCounter(int maxHealth)
    {
        HealthBar.MaxValue = Convert.ToDouble(maxHealth);
        this.maxHealth = maxHealth;
    }

    public void SetHealthCounter(int health)
    {
        HealthCounter.Text = health.ToString() + "/" + maxHealth;
        HealthBar.Value = Convert.ToDouble(health);
    }

    //TODO: Score system
    public void AddToScoreCounter(int points)
    {
        score += points;
        string scoreText = score.ToString();
        scoreText = String.Concat(Enumerable.Repeat("0", 8 - scoreText.Length)) + scoreText;
        ScoreCounter.Text = score.ToString();
    }

    public void SetEnemyCounter(int enemyCount)
    {
        EnemyCounter.Text = enemyCount.ToString() + " left";
    }
}

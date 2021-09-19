using Godot;
using System;
using System.Linq;

public class GUI : CanvasLayer
{
    Label HealthCounter;
    Label ShieldCounter;
    Bar HealthBar;
    Bar ShieldBar;
    Label ScoreCounter;
    Label EnemyCounter;
    int maxHealth;
    int maxShield;
    public int score = 0;

    public override void _Ready()
    {
        HealthCounter = GetNode<Label>("MarginContainer/HBoxContainer/Bars/LifeBar/TextureProgress/Number");
        ShieldCounter = GetNode<Label>("MarginContainer/HBoxContainer/Bars/ShieldBar/TextureProgress/Number");
        HealthBar = GetNode<Bar>("MarginContainer/HBoxContainer/Bars/LifeBar");
        ShieldBar = GetNode<Bar>("MarginContainer/HBoxContainer/Bars/ShieldBar");
        ScoreCounter = GetNode<Label>("MarginContainer/HBoxContainer/Counters/ScoreCounter/Background/Number");
        EnemyCounter = GetNode<Label>("MarginContainer/HBoxContainer/Counters/EnemyCounter/Background/Number");
        SetMaxHealthCounter(100);
    }

    public void SetMaxHealthCounter(int maxHealth)
    {
        HealthBar.SetMaxValue(maxHealth);
        this.maxHealth = maxHealth;
    }

    public void SetHealthCounter(int health)
    {
        HealthCounter.Text = health.ToString() + "/" + maxHealth;
        HealthBar.SetValue(health);
    }

    public void SetMaxShieldCounter(int maxShield)
    {
        ShieldBar.SetMaxValue(maxShield);
        this.maxShield = maxShield;
    }

    public void SetShieldCounter(int shield)
    {
        ShieldCounter.Text = shield.ToString() + "/" + maxShield;
        ShieldBar.SetValue(shield);
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

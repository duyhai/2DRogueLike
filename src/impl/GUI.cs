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
    Node2D crosshair;
    Vector2 lastMousePosition;
    bool isLastInputJoystick;
    int maxHealth;
    int maxShield;
    public int score = 0;
    const float DEFAULT_DELTA = 0.1f;

    public override void _Ready()
    {
        HealthCounter = GetNode<Label>("MarginContainer/HBoxContainer/Bars/LifeBar/TextureProgress/Number");
        ShieldCounter = GetNode<Label>("MarginContainer/HBoxContainer/Bars/ShieldBar/TextureProgress/Number");
        HealthBar = GetNode<Bar>("MarginContainer/HBoxContainer/Bars/LifeBar");
        ShieldBar = GetNode<Bar>("MarginContainer/HBoxContainer/Bars/ShieldBar");
        ScoreCounter = GetNode<Label>("MarginContainer/HBoxContainer/Counters/ScoreCounter/Background/Number");
        EnemyCounter = GetNode<Label>("MarginContainer/HBoxContainer/Counters/EnemyCounter/Background/Number");
        crosshair = GetNode<Sprite>("Crosshair");
        SetMaxHealthCounter(100);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        Vector2 result = crosshair.GlobalPosition;
        Vector2 joystickDirection = Input.GetVector("see_left", "see_right", "see_up", "see_down");
        bool isGamePadJoystickMoving = DEFAULT_DELTA < joystickDirection.Length();

        Vector2 mousePosition = crosshair.GetGlobalMousePosition();
        Vector2 dMouse = mousePosition - lastMousePosition;
        float dMousePosition = isLastInputJoystick? 10f : DEFAULT_DELTA;
        bool isMouseMoving = dMousePosition < dMouse.Length();
        if (isGamePadJoystickMoving)
        {
            result = (GetViewport().Size / 2) + (joystickDirection.Normalized() * 100);
            isLastInputJoystick = true;
        }
        else if (isMouseMoving)
        {
            result = mousePosition;
            isLastInputJoystick = false;
            lastMousePosition = mousePosition;
        }
        crosshair.GlobalPosition = result;
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

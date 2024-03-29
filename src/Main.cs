using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
    Map map;
    readonly int MAP_SIZE = 50;
    readonly int BLOCK_SIZE = 16;
    public static string SCENE_PATH = "res://scenes/Main.tscn";
    public Node World;
    Player player;
    GUI gui;
    Control resultScreen;
    Control deathScreen;

    int currentLevel = 0;
    List<Func<Map>> levelGenerators;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        World = GetNode<Node>("World");
        levelGenerators = new List<Func<Map>>() {
            // () => new PremadeMap((PackedScene)GD.Load("res://scenes/Map/Maps/Map1.tscn"), World),
            // () => new PremadeMap((PackedScene)GD.Load("res://scenes/Map/Maps/TestMap.tscn"), World),
            () => new AutoGeneratedMap(MAP_SIZE, MAP_SIZE, BLOCK_SIZE, World),
            () => new AutoGeneratedMap(MAP_SIZE, MAP_SIZE, BLOCK_SIZE, World),
            () => new AutoGeneratedMap(MAP_SIZE, MAP_SIZE, BLOCK_SIZE, World),
        };
        map = levelGenerators[currentLevel]?.Invoke();
        map.Instance();
        player = GetNode<Player>("World/Player");
        gui = GetNode<GUI>("GUI");
        resultScreen = GetNode<Control>("ResultScreen/ResultScreenNode");
        deathScreen = GetNode<Control>("DeathScreen/DeathScreenNode");
        FCTManager.Instance.TextContainer = GetNode<Node>("FloatingTexts");
        SoundManager.Instance.SoundContainer = GetNode<Node>("Sounds");

        // Weapon change delegate from weapon bar call when player changes weapon
        WeaponBar weaponBar = gui.GetNode<WeaponBar>("WeaponBar");
        player.WeaponChanged += weaponBar.WeaponChanged;
        player.WeaponListChanged += weaponBar.WeaponListChanged;
        // Initialize weapon bar
        weaponBar.WeaponListChanged(player.weapons);
        weaponBar.WeaponChanged(player.weapons[player.equippedWeaponIndex]);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        gui.SetHealthCounter(player.health);
        gui.SetEnemyCounter(CountEnemies());
        gui.SetMaxHealthCounter(player.Stats.MaxHealth);
        gui.SetMaxShieldCounter(player.Stats.MaxShield);
        gui.SetShieldCounter(player.Shield);

        if (CountEnemies() <= 0)
        {
            resultScreen.Visible = true;
            player.DisableInput = true;
        }
    }

    int CountEnemies()
    {
        int count = 0;
        foreach (var i in World.GetChildren())
        {
            Node node = (Node)i;
            var nodeClass = node.GetType().BaseType;
            if (nodeClass == typeof(Enemy))
            {
                count++;
            }
        }
        return count;
    }

    private void ResetWorld()
    {
        foreach (var i in World.GetChildren())
        {
            Node node = (Node)i;
            if (node.Name != "Player")
                node.QueueFree();
        }
        map = levelGenerators[currentLevel]?.Invoke();
        map.Instance();
        player.Respawn(map.PlayerSpawn.X * map.Unit, map.PlayerSpawn.Y * map.Unit);
    }

    public void OnNextLevelButtonPressed()
    {
        currentLevel++;
        ResetWorld();
        resultScreen.Visible = false;
    }

    public void OnNewGameButtonPressed()
    {
        ResetWorld();
        deathScreen.Visible = false;
    }

    public void OnRespawnButtonPressed()
    {
        player.Respawn(map.PlayerSpawn.X * map.Unit, map.PlayerSpawn.Y * map.Unit);
        deathScreen.Visible = false;
    }

    public void OnPlayerDeathSignal()
    {
        deathScreen.Visible = true;
    }
}

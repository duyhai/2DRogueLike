using Godot;

public class Main : Node
{
    Map map;
    readonly int MAP_SIZE = 30;
    readonly int BLOCK_SIZE = 16;
    public static string SCENE_PATH = "res://Main.tscn";
    public Node World;
    Player player;
    GUI gui;
    Control resultScreen;
    Control deathScreen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        World = GetNode<Node>("World");
        map = new AutoGeneratedMap(MAP_SIZE, MAP_SIZE, BLOCK_SIZE, World);
        map.Instance();
        player = GetNode<Player>("World/Player");
        gui = GetNode<GUI>("GUI");
        resultScreen = GetNode<Control>("ResultScreen/ResultScreenNode");
        deathScreen = GetNode<Control>("DeathScreen/DeathScreenNode");
        FCTManager.Instance.TextContainer = GetNode<Node>("FloatingTexts");
        SoundManager.Instance.SoundContainer = GetNode<Node>("Sounds");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        gui.SetHealthCounter(player.health);
        gui.SetEnemyCounter(CountEnemies());
        gui.SetMaxHealthCounter(player.maxHealth);

        if (CountEnemies() <= 0)
        {
            resultScreen.Visible = true;
            player.disableInput = true;
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
        map = new AutoGeneratedMap(MAP_SIZE, MAP_SIZE, BLOCK_SIZE, World);
        player.Respawn(map.PlayerSpawn.x * map.Unit, map.PlayerSpawn.y * map.Unit);
        map.Instance();
    }

    public void OnNextLevelButtonPressed()
    {
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
        player.Respawn(map.PlayerSpawn.x * map.Unit, map.PlayerSpawn.y * map.Unit);
        deathScreen.Visible = false;
    }

    public void OnPlayerDeathSignal()
    {
        deathScreen.Visible = true;
    }
}

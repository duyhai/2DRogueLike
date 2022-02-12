using Godot;
using System.Collections.Generic;

public enum MapTile
{
    EMPTY,
    WALL,
    BREAKABLE_WALL,
    SPEEDBOOST,
    DAMAGEBOOST,
    LIFESTEALBOOST,
    SIMPLESHIELDBOOST
}

public class Map
{
    static PackedScene wallBlockScene = UnbreakableWall.SceneObject;
    static PackedScene breakableWallScene = BreakableWall.SceneObject;
    static PackedScene speedBoostScene = SpeedBoost.SceneObject;
    static PackedScene damageBoostScene = DamageBoost.SceneObject;
    static PackedScene lifestealBoostScene = LifestealBoost.SceneObject;
    static PackedScene simpleShieldBoostScene = SimpleShieldBoost.SceneObject;
    static PackedScene playerScene = Player.SceneObject;

    Dictionary<MapTile, PackedScene> sceneMapping = new Dictionary<MapTile, PackedScene> {
        { MapTile.EMPTY, null },
        { MapTile.WALL, wallBlockScene },
        { MapTile.BREAKABLE_WALL, breakableWallScene },
        { MapTile.SPEEDBOOST, speedBoostScene },
        { MapTile.DAMAGEBOOST, damageBoostScene },
        { MapTile.LIFESTEALBOOST, lifestealBoostScene },
        { MapTile.SIMPLESHIELDBOOST, simpleShieldBoostScene }
    };

    public static readonly MapTile[] Boosters = new MapTile[] {
        MapTile.SPEEDBOOST,
        MapTile.DAMAGEBOOST,
        MapTile.LIFESTEALBOOST,
        MapTile.SIMPLESHIELDBOOST
    };

    public static List<PackedScene> EnemyScenes = new List<PackedScene> {
        Floater.SceneObject,
        Shooter.SceneObject,
        Splitter.SceneObject,
        Tank.SceneObject,
        Boomer.SceneObject
    };

    public int Width
    {
        get
        {
            return this.Blocks[0].Count;
        }
    }

    public int Height
    {
        get
        {
            return this.Blocks.Count;
        }
    }

    public int Unit;
    public List<List<MapTile>> Blocks;
    public List<List<PackedScene>> Enemies;
    public Vector2 PlayerSpawn;

    private Node parentNode;

    public Map(int width, int height, int unit, Node parentNode)
    {
        this.Unit = unit;
        this.parentNode = parentNode;
        this.Blocks = new List<List<MapTile>>();
        this.Enemies = new List<List<PackedScene>>();
        for (int i = 0; i < height; ++i)
        {
            this.Blocks.Add(new List<MapTile>());
            this.Enemies.Add(new List<PackedScene>());
            for (int j = 0; j < width; ++j)
            {
                this.Blocks[i].Add(MapTile.EMPTY);
                this.Enemies[i].Add(null);
            }
        }
    }

    virtual public void Instance()
    {
        for (int i = 0; i < Height; ++i)
        {
            for (int j = 0; j < Width; ++j)
            {
                createBlock(i, j, Unit, Blocks[i][j]);
                createEnemy(i, j, Unit, Enemies[i][j]);
            }
        }

        createPlayer(PlayerSpawn.x, PlayerSpawn.y, Unit);
    }

    private void createBlock(float x, float y, int blockSize, MapTile mapTile)
    {
        var tileScene = sceneMapping[mapTile];
        if (tileScene == null)
        {
            return;
        }

        var blockInstance = (Block)tileScene.Instance();
        parentNode.AddChild(blockInstance);

        // Set the mob's position to a random location.
        blockInstance.Position = new Vector2(x * blockSize, y * blockSize);
        blockInstance.Scale = new Vector2(1, 1);
    }

    private void createEnemy(float x, float y, int blockSize, PackedScene enemyScene)
    {
        if (enemyScene == null)
        {
            return;
        }

        var enemyInstance = (Enemy)enemyScene.Instance();
        parentNode.AddChild(enemyInstance);

        // Set the mob's position to a random location.
        enemyInstance.Position = new Vector2(x * blockSize, y * blockSize);
    }

    private void createPlayer(float x, float y, int blockSize)
    {
        var playerInstance = (Player)parentNode.GetNodeOrNull("Player");
        if (playerInstance == null)
        {
            playerInstance = (Player)playerScene.Instance();
            playerInstance.Name = "Player";
            parentNode.AddChild(playerInstance);
            playerInstance.Connect("DeathSignal", parentNode.GetParent(), "OnPlayerDeathSignal");
        }
        playerInstance.Position = new Vector2(x * blockSize, y * blockSize);
    }
}

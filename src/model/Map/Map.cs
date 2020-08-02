using Godot;
using System.Collections.Generic;

public enum MapTile
{
    EMPTY,
    WALL,
    BREAKABLE_WALL
}

public class Map
{
    static PackedScene wallBlockScene = (PackedScene)GD.Load("res://WallBlock.tscn");
    static PackedScene breakableWallScene = (PackedScene)GD.Load("res://BreakableWall.tscn");
    Dictionary<MapTile, PackedScene> sceneMapping = new Dictionary<MapTile, PackedScene> {
        { MapTile.EMPTY, null },
        { MapTile.WALL, wallBlockScene },
        { MapTile.BREAKABLE_WALL, breakableWallScene }
    };

    public static List<PackedScene> EnemyScenes = new List<PackedScene> {
        Floater.SceneObject
    };

    public int Width {
        get {
            return this.Blocks[0].Count;
        }
    } 

    public int Height {
        get {
            return this.Blocks.Count;
        }
    }

	public int Unit;
	public List<List<MapTile>> Blocks;
	public List<List<PackedScene>> Enemies;
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

    virtual public void Instance(){
        for (int i = 0; i < Height; ++i)
        {
            for (int j = 0; j < Width; ++j)
            {
                createBlock(i, j, Unit, Blocks[i][j]);  
                createEnemy(i, j, Unit, Enemies[i][j]);  
            }
        }
    }

    private void createBlock(float x, float y, int blockSize, MapTile mapTile){
        var tileScene = sceneMapping[mapTile];
        if (tileScene == null) return;

		var blockInstance = (StaticBody2D)tileScene.Instance();
		this.parentNode.AddChild(blockInstance);

		// Set the mob's position to a random location.
		blockInstance.Position = new Vector2(x * blockSize, y * blockSize);
        blockInstance.Scale = new Vector2(1, 1);
    }

    private void createEnemy(float x, float y, int blockSize, PackedScene enemyScene){
        if (enemyScene == null) return;

		var enemyInstance = (KinematicBody2D)enemyScene.Instance();
		this.parentNode.AddChild(enemyInstance);

		// Set the mob's position to a random location.
		enemyInstance.Position = new Vector2(x * blockSize, y * blockSize);
        enemyInstance.Scale = new Vector2(1, 1);
    }
}

using Godot;
using System.Collections.Generic;

public enum MapTile
{
    EMPTY,
    WALL
}

public class Map
{       
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
    private PackedScene tileScene;
    private Node parentNode;

	public Map(int width, int height, int unit, Node parentNode, PackedScene tileScene)
	{
		this.Unit = unit;
        this.tileScene = tileScene;
        this.parentNode = parentNode;
        this.Blocks = new List<List<MapTile>>();
        for (int i = 0; i < height; ++i)
        {
            this.Blocks.Add(new List<MapTile>());
            for (int j = 0; j < width; ++j)
            {
                this.Blocks[i].Add(MapTile.EMPTY);
            }
        }
	}

    virtual public void Instance(){
        for (int i = 0; i < Height; ++i)
        {
            for (int j = 0; j < Width; ++j)
            {
                if (Blocks[i][j] == MapTile.WALL)
                {
                    createBlock(i, j, Unit);    
                }
            }
        }
    }

    private void createBlock(float x, float y, int blockSize){
		var blockInstance = (StaticBody2D)this.tileScene.Instance();
		this.parentNode.AddChild(blockInstance);

		// Set the mob's position to a random location.
		blockInstance.Position = new Vector2(x * blockSize, y * blockSize);
        blockInstance.Scale = new Vector2(1, 1);
    }
}

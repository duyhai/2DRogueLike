using System;
using System.Collections.Generic;
using Godot;


class CaveGenerator : Generator
{
    const int WALLCH = 40;
    const int CHANGEWALLCH = 80;

    private struct MapBlockInfo
    {
        public MapTile tile;
        public int x;
        public int y;
    }

    public CaveGenerator(Map map) : base(map, 0, 0, 0) { }

    private int wallsNearby(Map map, int x, int y, int radius)
    {
        int beginX = x - radius;
        int endX = x + radius;
        int beginY = y - radius;
        int endY = y + radius;
        int count = 0;

        foreach (MapBlockInfo block in mapBlocks(map, beginX, endX, beginY, endY))
        {
            count += isWall(block.tile) ? 1 : 0;
        }

        return count;
    }

    private IEnumerable<MapBlockInfo> mapBlocks(Map map, int beginX, int endX, int beginY, int endY)
    {
        for (int i = beginX; i <= endX; i++)
        {
            for (int j = beginY; j <= endY; j++)
            {
                yield return new MapBlockInfo
                {
                    tile = isOutOfBounds(map, i, j) ? MapTile.WALL : map.Blocks[i][j],
                    x = i,
                    y = j
                };
            }
        }
    }

    private bool isWall(MapTile tile)
    {
        return tile != MapTile.EMPTY;
    }

    private bool isOutOfBounds(Map map, int x, int y)
    {
        return y < 0 || map.Width <= y || x < 0 || map.Height <= x;
    }

    // Averages the walls types near the (x, y) coordinates
    // by taking the walls indexes from the Map.Maptiles array
    private int averageWallsNearby(Map map, int row, int column, int radius)
    {
        int beginX = row - radius;
        int endX = row + radius;
        int beginY = column - radius;
        int endY = column + radius;
        int num = 0;
        float count = 0f;

        foreach (var block in mapBlocks(map, beginX, endX, beginY, endY))
        {
            if (block.tile != MapTile.EMPTY)
            {
                count += Array.FindIndex(Map.WallTypes, wall => wall == block.tile);
                num++;
            }
        }

        return (int)Math.Round(count / num);
    }

    override public void GenerateMap()
    {
        int width = map.Width;
        int height = map.Height;

        Map[] tempMaps = new Map[2];
        tempMaps[0] = new Map(width, height, map.Unit, null);
        tempMaps[1] = new Map(width, height, map.Unit, null);

        // Walls on the edge of the map
        for (int i = 0; i < height; i++)
        {
            tempMaps[0].Blocks[i][0] = MapTile.WALL;
            tempMaps[1].Blocks[i][width - 1] = MapTile.WALL;
        }
        for (int j = 0; j < width; j++)
        {
            tempMaps[0].Blocks[0][j] = MapTile.WALL;
            tempMaps[1].Blocks[height - 1][j] = MapTile.WALL;
        }

        // Placing random walls
        Random rnd = new Random();
        for (int i = 1; i < height - 1; i++)
        {
            for (int j = 1; j < width - 1; j++)
            {
                tempMaps[0].Blocks[i][j] = rnd.Next(1, 101) < WALLCH ? MapTile.WALL : MapTile.EMPTY;
            }
        }

        // Using cellular automata to generate cave-like structures
        // http://roguebasin.com/?title=Cellular_Automata_Method_for_Generating_Random_Cave-Like_Levels
        int phase = 0;
        for (int k = 0; k < 4; k++)
        {
            int currphase = phase % 2;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    bool makewall = wallsNearby(tempMaps[currphase], i, j, 1) >= 5 || wallsNearby(tempMaps[currphase], i, j, 2) <= 3;
                    tempMaps[(phase + 1) % 2].Blocks[i][j] = makewall ? MapTile.WALL : MapTile.EMPTY;
                }
            }
            phase++;
        }

        for (int k = 0; k < 4; k++)
        {
            int currphase = phase % 2;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    bool makewall = wallsNearby(tempMaps[currphase], i, j, 1) >= 5;
                    tempMaps[(phase + 1) % 2].Blocks[i][j] = makewall ? MapTile.WALL : MapTile.EMPTY;
                }
            }
            phase++;
        }

        // Changes the walls to a random type of wall
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (tempMaps[phase % 2].Blocks[i][j] == MapTile.WALL)
                {
                    tempMaps[phase % 2].Blocks[i][j] = Map.WallTypes[rnd.Next(0, Map.WallTypes.Length)];
                }
            }
        }

        // Averaging walls to make cohesive wall sections
        for (int k = 0; k < 4; k++)
        {
            int currphase = phase % 2;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = Array.FindIndex(Map.WallTypes, x => x == tempMaps[currphase].Blocks[i][j]);
                    if (index != -1)
                    {
                        int val = index;
                        tempMaps[(phase + 1) % 2].Blocks[i][j] = rnd.Next(0, 101) < CHANGEWALLCH ?
                            Map.WallTypes[index] :
                            Map.WallTypes[averageWallsNearby(tempMaps[currphase], i, j, 3)];
                    }
                }
            }
            phase++;
        }

        // Copying the result into the map variable
        int currentphase = phase % 2;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map.Blocks[i][j] = tempMaps[currentphase].Blocks[i][j];
            }
        }

        // Enemies
        foreach (var block in mapBlocks(map, 0, width, 0, height))
        {
            if (!isWall(block.tile))
            {
                // TODO: Create a better spawn system. Separate enemy generator class? Should be able to limit enemies per room and per map. Set difficulty limit, etc.
                var spawnChance = 3;
                var shouldSpawn = rnd.Next(0, 100) < spawnChance;
                if (shouldSpawn)
                {
                    map.Enemies[block.x][block.y] = Map.EnemyScenes[rnd.Next(0, Map.EnemyScenes.Count)];
                }
            }

        }

        // Playerspawn
        int a = 0;
        int b = 0;
        while (isWall(map.Blocks[a][b]))
        {
            a = rnd.Next(0, width);
            b = rnd.Next(0, height);
        }
        map.PlayerSpawn.X = a;
        map.PlayerSpawn.Y = b;
    }
}

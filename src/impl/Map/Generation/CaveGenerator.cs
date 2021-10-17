using System;
using System.Collections.Generic;
using Godot;


class CaveGenerator : Generator
{
    List<Room> RoomList = new List<Room>();
    List<Corridor> CorridorList = new List<Corridor>();
    Random rnd = new Random();

    public CaveGenerator(Map map) : base(map, 0, 0, 0)
    {
    }

    Room GenerateRoom()
    {
        int x, y, w, h;
        w = rnd.Next(MinRoomXY, MaxRoomXY);
        h = rnd.Next(MinRoomXY, MaxRoomXY);
        x = rnd.Next(0, map.Width - w);
        y = rnd.Next(0, map.Height - h);
        return new Room(x, y, w, h);
    }

    private int wallsNearby(Map map, int x, int y, int radius)
    {
        int beginX = x - radius;
        int endX = x + radius;
        int beginY = y - radius;
        int endY = y + radius;
        int count = 0;

        for (int i = beginX; i <= endX; i++)
        {
            for (int j = beginY; j <= endY; j++)
            {
                count += isWall(map, i, j) ? 1 : 0;
            }
        }

        return count;
    }

    private bool isWall(Map map, int x, int y)
    {
        if (isOutOfBounds(map, x, y))
        {
            return true;
        }

        return map.Blocks[x][y] != MapTile.EMPTY;
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

        for (int i = beginX; i <= endX; i++)
        {
            for (int j = beginY; j <= endY; j++)
            {
                if (!isOutOfBounds(map, i, j) && map.Blocks[i][j] != MapTile.EMPTY)
                {
                    count += Array.FindIndex(Map.WallTypes, wall => wall == map.Blocks[i][j]);
                    num++;
                }
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

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                {
                    tempMaps[0].Blocks[i][j] = MapTile.WALL;
                    tempMaps[1].Blocks[i][j] = MapTile.WALL;
                }
                else
                {
                    tempMaps[0].Blocks[i][j] = MapTile.EMPTY;
                    tempMaps[1].Blocks[i][j] = MapTile.EMPTY;
                }
            }
        }

        Random rnd = new Random();
        for (int i = 1; i < height - 1; i++)
        {
            for (int j = 1; j < width - 1; j++)
            {
                tempMaps[0].Blocks[i][j] = rnd.Next(1, 101) < 40 ? MapTile.WALL : MapTile.EMPTY;
            }
        }

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
                        int changewallch = 80;
                        tempMaps[(phase + 1) % 2].Blocks[i][j] = rnd.Next(0, 101) < changewallch ?
                            Map.WallTypes[index] :
                            Map.WallTypes[averageWallsNearby(tempMaps[currphase], i, j, 3)];
                    }
                }
            }
            phase++;
        }

        int currentphase = phase % 2;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map.Blocks[i][j] = tempMaps[currentphase].Blocks[i][j];
            }
        }

        // Enemies
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!isWall(map, i, j))
                {
                    // TODO: Create a better spawn system. Separate enemy generator class? Should be able to limit enemies per room and per map. Set difficulty limit, etc.
                    var spawnChance = 3;
                    var shouldSpawn = rnd.Next(0, 100) < spawnChance;
                    if (shouldSpawn)
                    {
                        map.Enemies[i][j] = Map.EnemyScenes[rnd.Next(0, Map.EnemyScenes.Count)];
                    }
                }
            }
        }

        // Playerspawn
        int a = 0;
        int b = 0;
        while (isWall(map, a, b))
        {
            a = rnd.Next(0, width);
            b = rnd.Next(0, height);
        }
        map.PlayerSpawn.x = a;
        map.PlayerSpawn.y = b;


    }
}
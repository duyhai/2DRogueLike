using System;
using System.Collections.Generic;
using Godot;


<<<<<<< HEAD
class GeneratorV1: Generator
{    List<Room> RoomList = new List<Room>();
    List<Corridor> CorridorList = new List<Corridor>();
    Random rnd = new Random();

    public GeneratorV1(Map map, int MaxRooms, int MinRoomXY, int MaxRoomXY): base(map, MaxRooms, MinRoomXY, MaxRoomXY)
=======
class GeneratorV1 : Generator
{
    List<Room> RoomList = new List<Room>();
    List<Corridor> CorridorList = new List<Corridor>();
    Random rnd = new Random();

    public GeneratorV1(Map map, int MaxRooms, int MinRoomXY, int MaxRoomXY) : base(map, MaxRooms, MinRoomXY, MaxRoomXY)
>>>>>>> master
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

    bool RoomOverlapping(Room room)
    {
        return RoomList.Exists(currentRoom => room.Intersect(currentRoom));
    }

    void CorridorBetweenPoints(int x1, int y1, int x2, int y2)
    {
        if (x1 == x2 || y1 == y2)
        {
            CorridorList.Add(new Corridor(new Vector2(x1, y1), new Vector2(x2, y2)));
        }
        else
        {
            int rndNumber = rnd.Next(0, 2);
            if (rndNumber == 0)
            {
                CorridorList.Add(new Corridor(new Vector2(x1, y1), new Vector2(x1, y2)));
                CorridorList.Add(new Corridor(new Vector2(x1, y2), new Vector2(x2, y2)));
            }
            else
            {
                CorridorList.Add(new Corridor(new Vector2(x1, y1), new Vector2(x2, y1)));
                CorridorList.Add(new Corridor(new Vector2(x2, y1), new Vector2(x2, y2)));
            }
        }
    }

    void JoinRooms(Room room1, Room room2)
    {
        int x1 = room1.x;
        int y1 = room1.y;
        int w1 = room1.w;
        int h1 = room1.h;

        int x2 = room2.x;
        int y2 = room2.y;
        int w2 = room2.w;
        int h2 = room2.h;

        int p1X = rnd.Next(x1, x1 + w1);
        int p1Y = rnd.Next(y1, y1 + h1);

        int p2X = rnd.Next(x2, x2 + w2);
        int p2Y = rnd.Next(y2, y2 + h2);

        CorridorBetweenPoints(p1X, p1Y, p2X, p2Y);
    }

    override public void GenerateMap()
    {
        RoomList.Clear();
        CorridorList.Clear();

        for (int i = 0; i < MaxRooms; i++)
        {
            Room tempRoom = GenerateRoom();
            while (RoomOverlapping(tempRoom))
            {
                tempRoom = GenerateRoom();
            }
            RoomList.Add(tempRoom);
        }

        for (int i = 0; i < RoomList.Count - 1; i++)
        {
            JoinRooms(RoomList[i], RoomList[i + 1]);
        }

        for (int i = 0; i < map.Height; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
<<<<<<< HEAD
                map.Blocks[i][j] = rnd.Next(0,100) < 50 ? MapTile.BREAKABLE_WALL : MapTile.WALL;
            }
        }

        foreach (var room in RoomList)
        {
=======
                map.Blocks[i][j] = rnd.Next(0, 100) < 50 ? MapTile.BREAKABLE_WALL : MapTile.WALL;
            }
        }

        int playerSpawningRoom = rnd.Next(0, RoomList.Count); //Room index where player will spawn
        for (int i = 0; i < RoomList.Count; i++)
        {
            Room room = RoomList[i];
>>>>>>> master
            int x = room.x;
            int y = room.y;
            int w = room.w;
            int h = room.h;
<<<<<<< HEAD
            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + h; j++)
                {
                    map.Blocks[i][j] = MapTile.EMPTY;
                    // TODO: Create a better spawn system. Separate enemy generator class? Should be able to limit enemies per room and per map. Set difficulty limit, etc.
                    var spawnChance = 3;
                    var shouldSpawn = rnd.Next(0, 100) < spawnChance;
                    if (shouldSpawn) {
                        map.Enemies[i][j] = Map.EnemyScenes[rnd.Next(0, Map.EnemyScenes.Count)];
=======

            if (i == playerSpawningRoom)
            {
                int a, b;
                a = rnd.Next(x, x + w);
                b = rnd.Next(y, y + h);

                map.PlayerSpawn.x = a;
                map.PlayerSpawn.y = b;
            }
            for (int j = x; j < x + w; j++)
            {
                for (int k = y; k < y + h; k++)
                {
                    map.Blocks[j][k] = MapTile.EMPTY;
                    // TODO: Create a better spawn system. Separate enemy generator class? Should be able to limit enemies per room and per map. Set difficulty limit, etc.
                    var spawnChance = i == playerSpawningRoom ? 0 : 3;
                    var shouldSpawn = rnd.Next(0, 100) < spawnChance;
                    if (shouldSpawn)
                    {
                        map.Enemies[j][k] = Map.EnemyScenes[rnd.Next(0, Map.EnemyScenes.Count)];
>>>>>>> master
                    }
                }
            }
        }
        foreach (var corridor in CorridorList)
        {
            int x1 = (int)corridor.Start.x;
            int y1 = (int)corridor.Start.y;
            int x2 = (int)corridor.End.x;
            int y2 = (int)corridor.End.y;
            for (int i = 0; i < Math.Abs(x1 - x2) + 1; i++)
            {
                for (int j = 0; j < Math.Abs(y1 - y2) + 1; j++)
                {
                    int corridorBlockX = Math.Min(x1, x2) + i;
                    int corridorBlockY = Math.Min(y1, y2) + j;
                    map.Blocks[corridorBlockX][corridorBlockY] = MapTile.EMPTY;
                }
            }
        }
    }
}

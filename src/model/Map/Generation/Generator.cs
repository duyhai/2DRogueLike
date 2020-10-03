abstract public class Generator
{
    protected int MaxRooms;
    protected int MinRoomXY;
    protected int MaxRoomXY;
    protected Map map;

    public Generator(Map map, int MaxRooms, int MinRoomXY, int MaxRoomXY)
    {
        this.MaxRooms = MaxRooms;
        this.MinRoomXY = MinRoomXY;
        this.MaxRoomXY = MaxRoomXY;
        this.map = map;
    }

    abstract public void GenerateMap();
}


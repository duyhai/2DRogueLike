class Room
{
    public int x, y, w, h;
    public Room(int x, int y, int w, int h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }

    public bool Intersect(Room room)
    {
        return x < (room.x + room.w) &&
                room.x < (x + w) &&
                y < (room.y + room.h) &&
                room.y < (y + h);
    }
}


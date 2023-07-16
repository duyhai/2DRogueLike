using Godot;

public partial class BreakableWall : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Map/Blocks/BreakableWall.tscn");
    public BreakableWall() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {
        Textures = new Texture2D[] {
            ResourceLoader.Load<Texture2D>("res://assets/blocks/breakablewall/breakablewall1.png"),
            ResourceLoader.Load<Texture2D>("res://assets/blocks/breakablewall/breakablewall2.png"),
            ResourceLoader.Load<Texture2D>("res://assets/blocks/breakablewall/breakablewall3.png"),
        };
    }

    public override int Hit(int damage)
    {
        QueueFree();
        return 0;
    }
}
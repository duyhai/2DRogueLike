using Godot;

public abstract class Block : GameObject
{
    public Block(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    {

    }

    public override void _Ready()
    {
        CollisionLayer = CollisionLayers.MapObject;
    }
}
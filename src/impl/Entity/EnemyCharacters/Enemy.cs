using Godot;

public class Enemy : GameObject
{
    public Enemy(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    {

    }

    public override void _Ready()
    {
        base._Ready();
        AddToGroup("enemy");
    }
}
using Godot;

public abstract class GameObject : KinematicBody2D
{
	public Vector2 velocity;
	public int speed;
	protected InputController inputController;
	protected PhysicsController physicsController;
	protected GraphicsController graphicsController;

    public GameObject(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController){
        this.inputController = inputController;
        this.physicsController = physicsController;
        this.graphicsController = graphicsController;
    }

	public override void _Process(float delta)
	{
		inputController.Update(this);
		graphicsController.Update(this);
	}

	public override void _PhysicsProcess(float delta)
	{
		physicsController.Update(this, delta);
	}
}

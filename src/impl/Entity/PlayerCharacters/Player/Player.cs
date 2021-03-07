using Godot;

public class Player : GameObject
{
    public Weapon weapon;

    public static PackedScene SceneObject = (PackedScene)GD.Load("res://Player.tscn");

    public Player() :
        base(new PlayerInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        this.speed = 200;
        this.maxHealth = 100;
        this.health = maxHealth;
        CollisionLayer = CollisionLayers.Player;
        CollisionMask = CollisionLayers.Enemy | CollisionLayers.MapObject;
    }

    public override void _Ready()
    {
        weapon = GetNode<SimpleWeapon>("SimpleWeapon");
    }

    public void Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        weapon.Shoot(vector, collisionLayer, collisionMask);
    }

    public void Respawn(float x, float y)
    {
        ResetHealth();
        disableInput = false;
        Position = new Vector2(x, y);
        ((BasicGraphicsController)graphicsController).ResetSprite(this);
    }

    public override void OnAnimationFinished(string animationName) { }
}

using Godot;

public class Melee : Weapon
{
    public Melee() : base(new MeleeGraphicsController(), 25) { }
    Area2D swingingArea;

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        swingingArea = GetNode<Area2D>("Area2D");
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        swingingArea.CollisionLayer = collisionLayer;
        swingingArea.CollisionMask = collisionMask;
        if (!bulletTimer.IsStopped())
        {
            return false;
        }
        bulletTimer.Start();
        var bodies = swingingArea.GetOverlappingBodies();
        foreach (var body in bodies)
        {
            var method = body.GetType().GetMethod("Hit");
            method?.Invoke(body, new object[] { damage });
        }
        ((MeleeGraphicsController)graphicsController).Swing(this);

        return true;
    }
}
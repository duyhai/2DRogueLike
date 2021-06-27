using Godot;

public class Melee : Weapon
{
    public Melee() : base(new MeleeGraphicsController(), 25) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = MeleeBullet.SceneObject;
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (base.Shoot(vector, collisionLayer, collisionMask))
        {
            ((MeleeGraphicsController)graphicsController).Swing(this);
            return true;
        }
        return false;
    }
}
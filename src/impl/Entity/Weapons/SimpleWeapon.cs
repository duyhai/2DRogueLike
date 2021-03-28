using Godot;

public class SimpleWeapon : Weapon
{

    public SimpleWeapon() : base(new SimpleWeaponGraphicsController()) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = SimpleBullet.SceneObject;
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (base.Shoot(vector, collisionLayer, collisionMask))
        {
            ((SimpleWeaponGraphicsController)graphicsController).MuzzleFlash(this);
            return true;
        }
        return false;
    }
}

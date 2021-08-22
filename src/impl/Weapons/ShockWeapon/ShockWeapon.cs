using Godot;
using Godot.Collections;
using System.Linq;

public class ShockWeapon : Weapon
{
    public ShockWeapon() : base(new ShockWeaponGraphicsController(), 1000) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = LightningBullet.SceneObject;
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (base.Shoot(vector, collisionLayer, collisionMask))
        {
            SoundManager.Instance.PlaySound(SoundPaths.Lightning);
            return true;
        }

        return false;
    }
}

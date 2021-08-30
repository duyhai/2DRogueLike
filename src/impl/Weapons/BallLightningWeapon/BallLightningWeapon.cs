using Godot;
using Godot.Collections;
using System.Linq;

public class BallLightningWeapon : Weapon
{
    public BallLightningWeapon() : base(new WeaponGraphicsController(), 0.5f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = BallLightningBullet.SceneObject;
    }
}

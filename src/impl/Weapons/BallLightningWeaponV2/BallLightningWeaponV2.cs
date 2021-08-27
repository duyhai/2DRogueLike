using Godot;
using Godot.Collections;
using System.Linq;

public class BallLightningWeaponV2 : Weapon
{
    public BallLightningWeaponV2() : base(new WeaponGraphicsController(), 20) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = BallLightningBulletV2.SceneObject;
    }
}

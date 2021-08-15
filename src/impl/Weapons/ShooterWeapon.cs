using Godot;

public class ShooterWeapon : Weapon
{

    public ShooterWeapon() : base(null, 2f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = SimpleBullet.SceneObject;
    }

}

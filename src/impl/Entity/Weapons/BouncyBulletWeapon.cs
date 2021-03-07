using Godot;

public class BouncyBulletWeapon : Weapon
{
    public BouncyBulletWeapon() : base(new WeaponGraphicsController()) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = BouncyBullet.SceneObject;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

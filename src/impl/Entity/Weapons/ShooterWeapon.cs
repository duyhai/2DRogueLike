using Godot;

public class ShooterWeapon : Weapon
{

    public ShooterWeapon() : base(null) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = SimpleBullet.SceneObject;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

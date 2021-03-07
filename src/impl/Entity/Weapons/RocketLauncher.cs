using Godot;

public class RocketLauncher : Weapon
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = RocketProjectile.SceneObject;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

using Godot;

public class Melee : Weapon
{
    public Melee() : base(new MeleeGraphicsController(), 2.5f) { }

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = MeleeBullet.SceneObject;
    }

    public override bool Shoot(Vector2 vector)
    {
        if (base.Shoot(vector))
        {
            ((MeleeGraphicsController)graphicsController).Swing(this);
            return true;
        }
        return false;
    }
}
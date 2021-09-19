using Godot;

public class FreezingBullet : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/FreezingBullet.tscn");
    public bool Collided = false;

    public FreezingBullet() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new FreezingBulletGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, MaxShield = 0, Damage = 0, Speed = 75 };
    }

    public override void _Ready()
    {
    }

    public override int HitTarget(Node collider)
    {
        int inflictedDamage = base.HitTarget(collider);

        FreezingPowerUp freezingPowerUp = (FreezingPowerUp)GD.Load<PackedScene>("res://scenes/powerups/FreezingPowerUp.tscn").Instance();
        freezingPowerUp.Initiate((GameObject)collider, initiator);
        var method = collider.GetType().GetMethod("AddPowerUp");
        method?.Invoke(collider, new object[] { freezingPowerUp });

        QueueFree();
        return inflictedDamage;
    }

    public void OnAnimationPlayerAnimationFinished(string animationName)
    {
        if (animationName == "Fade")
        {
            QueueFree();
        }
    }
}
using Godot;

public class FlamethrowerFlame : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/FlamethrowerFlame.tscn");
    AnimationPlayer animationPlayer;

    public FlamethrowerFlame() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new FlamethrowerFlameGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 0, MaxShield = 0, Damage = 0, Speed = 75 };
    }

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        var controller = (FlamethrowerFlameGraphicsController)graphicsController;
        controller.PlayFadeAnimation(this);
        ((FlamethrowerFlameGraphicsController)graphicsController).RandomTexture(this);
    }

    public override int HitTarget(Node collider)
    {
        int inflictedDamage = base.HitTarget(collider);

        BurningPowerUp burningPowerUp = (BurningPowerUp)GD.Load<PackedScene>("res://scenes/powerups/BurningPowerUp.tscn").Instance();
        burningPowerUp.Initiate((GameObject)collider, initiator);
        var method = collider.GetType().GetMethod("AddPowerUp");
        method?.Invoke(collider, new object[] { burningPowerUp });

        QueueFree();
        return inflictedDamage;
    }

    public void OnAnimationPlayerAnimationFinished(string animationName)
    {
        if (animationName == "Flame")
        {
            QueueFree();
        }
    }
}
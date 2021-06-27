using Godot;

public class FlamethrowerFlame : Bullet
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/FlamethrowerFlame.tscn");
    AnimationPlayer animationPlayer;

    public FlamethrowerFlame() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new FlamethrowerFlameGraphicsController())
    {
        baseSpeed = 75;
    }

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        var controller = (FlamethrowerFlameGraphicsController)graphicsController;
        controller.PlayFadeAnimation(this);
        ((FlamethrowerFlameGraphicsController)graphicsController).RandomTexture(this);
    }

    public override int HitTarget(KinematicCollision2D collision)
    {
        int inflictedDamage = base.HitTarget(collision);
        Node collider = (Node)collision.Collider;

        if (inflictedDamage > 0)
        {
            BurningPowerUp burningPowerUp = (BurningPowerUp)GD.Load<PackedScene>("res://scenes/powerups/BurningPowerUp.tscn").Instance();
            var method = collider.GetType().GetMethod("AddPowerUp");
            method?.Invoke(collider, new object[] { burningPowerUp });
        }
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
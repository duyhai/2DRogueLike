using Godot;

public class FlamethrowerFlame : Bullet
{
    static int DAMAGE = 3;
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://FlamethrowerFlame.tscn");
    AnimationPlayer animationPlayer;

    public FlamethrowerFlame() :
        base(new NullInputController(), new SimpleBulletPhysicsController(), new FlamethrowerFlameGraphicsController(), DAMAGE)
    {
        speed = 75;
    }

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        var controller = (FlamethrowerFlameGraphicsController)graphicsController;
        controller.PlayFadeAnimation(this);

    }

    public override void HitTarget(KinematicCollision2D collision)
    {
        base.HitTarget(collision);
        QueueFree();
    }

    public void OnAnimationPlayerAnimationFinished(string animationName)
    {
        if (animationName == "Flame")
        {
            QueueFree();
        }
    }
}
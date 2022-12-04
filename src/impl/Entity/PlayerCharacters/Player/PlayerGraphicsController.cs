using Godot;

public class PlayerGraphicsController : BasicGraphicsController
{
    public CameraController CameraController
    {
        private get;
        set;
    }

    public override void Update(Node2D node)
    {
        base.Update(node);

        AnimatedSprite animSprite = node.GetNode<AnimatedSprite>("AnimatedSprite");

        Vector2 vector = node.GetGlobalMousePosition() - node.GlobalPosition;
        animSprite.FlipH = vector.x > 0;
        animSprite.Animation = "walk";
        animSprite.Playing = true;
    }

    public override void PlayHitAnimation(GameObject node)
    {
        base.PlayHitAnimation(node);

        if (CameraController != null)
        {
            PlayerCameraController playerCameraController = (PlayerCameraController)CameraController;

            HitCameraEffect hitEffect = HitCameraEffect.SceneObject.Instance<HitCameraEffect>();
            playerCameraController.AddCameraAffect(hitEffect);
            hitEffect.Start(node.GetNode<Camera2D>("Camera2D"));

            ShakeCameraEffect shakeEffect = new ShakeCameraEffect(duration: 0.5f, frequency: 40, amplitude: 5);
            playerCameraController.AddCameraAffect(shakeEffect);
            shakeEffect.Start(node.GetNode<Camera2D>("Camera2D"));
        }
    }
}
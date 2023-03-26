using Godot;

public partial class PlayerGraphicsController : BasicGraphicsController
{
    public CameraController CameraController
    {
        private get;
        set;
    }

    public override void Update(Node2D node)
    {
        base.Update(node);
        Player player = (Player) node;

        AnimatedSprite2D animSprite = node.GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        Vector2 vector = player.ViewDirection;
        animSprite.FlipH = vector.X > 0;
        animSprite.Play("walk");
    }

    public override void PlayHitAnimation(GameObject node)
    {
        base.PlayHitAnimation(node);

        if (CameraController != null)
        {
            PlayerCameraController playerCameraController = (PlayerCameraController)CameraController;

            HitCameraEffect hitEffect = HitCameraEffect.SceneObject.Instantiate<HitCameraEffect>();
            playerCameraController.AddCameraAffect(hitEffect);
            hitEffect.Start(node.GetNode<Camera2D>("Camera2D"));

            ShakeCameraEffect shakeEffect = new ShakeCameraEffect(duration: 0.5f, frequency: 40, amplitude: 5);
            playerCameraController.AddCameraAffect(shakeEffect);
            shakeEffect.Start(node.GetNode<Camera2D>("Camera2D"));
        }
    }
}
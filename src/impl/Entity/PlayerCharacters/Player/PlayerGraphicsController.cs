using Godot;

public class PlayerGraphicsController : BasicGraphicsController
{
    private CameraController cameraController;
    public CameraController CameraController
    {
        set
        {
            cameraController = value;
        }
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

        if (cameraController != null)
        {
            ShakeCameraEffect effect = new ShakeCameraEffect(duration: 0.5f, frequency: 40, amplitude: 5);
            ((PlayerCameraController)cameraController).AddCameraAffect(effect);  // casting???
            effect.Start();
        }
    }
}
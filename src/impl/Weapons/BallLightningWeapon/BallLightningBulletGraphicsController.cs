using Godot;
using Godot.Collections;

public class BallLightningBulletGraphicsController : GraphicsController
{
    PackedScene LightningScene = (PackedScene)GD.Load("res://scenes/weapons/projectiles/Lightning.tscn");

    public override void Update(Node2D node)
    {
        BallLightningBullet ballLightningBullet = (BallLightningBullet)node;
        if (ballLightningBullet.TargetingState == BallLightningBullet.Targeting.Animation)
        {
            ChainingBodiesAnimation(ballLightningBullet, ballLightningBullet.BodiesHit);
            ballLightningBullet.TargetingState = BallLightningBullet.Targeting.Initial;
            ballLightningBullet.BodiesHit.Clear();
        }
    }

    public void ChainingBodiesAnimation(Node2D node, Array bodiesHit)
    {
        foreach (Node2D body in bodiesHit)
        {
            ConnectLightning(node, node, body);
        }
    }

    private void ConnectLightning(Node2D node, Node2D o1, Node2D o2)
    {
        var lightning = (Lightning)LightningScene.Instance();

        // If one of the GameObject dies "too fast"(QueueFree() is called immidiately)
        // after LightningBulletPhysicsController.Update() is executed,
        // that GameObject becomes a null item in the lightningBullet.BodiesHit array
        // and it won't be able to get the global position of the GameObject
        lightning.AddPoint(o1.GlobalPosition);
        lightning.AddPoint(o2.GlobalPosition);
        node.GetParent().GetParent().AddChild(lightning);
        lightning.PlayAnimation();
    }
}
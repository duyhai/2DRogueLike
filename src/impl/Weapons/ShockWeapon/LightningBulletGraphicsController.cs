using Godot;
using Godot.Collections;

public class LightningBulletGraphicsController : GraphicsController
{
    PackedScene LightningScene = (PackedScene)GD.Load("res://scenes/weapons/projectiles/Lightning.tscn");

    public override void Update(Node2D node)
    {
        LightningBullet lightningBullet = (LightningBullet)node;
        if (lightningBullet.TargetingState == LightningBullet.Targeting.Animation)
        {
            ChainingBodiesAnimation(lightningBullet, lightningBullet.BodiesHit);
            lightningBullet.TargetingState = LightningBullet.Targeting.Initial;
            lightningBullet.BodiesHit.Clear();
        }
    }

    public void ChainingBodiesAnimation(Node2D node, Array bodiesHit)
    {
        if (bodiesHit.Count != 0)
        {
            ConnectLightning(node, node, (Node2D)bodiesHit[0]);
        }

        for (int i = 0; i < bodiesHit.Count - 1; i++)
        {
            ConnectLightning(node, (Node2D)bodiesHit[i], (Node2D)bodiesHit[i + 1]);
        }
    }

    private void ConnectLightning(Node2D node, Node2D o1, Node2D o2)
    {
        var lightning = (Lightning)LightningScene.Instance();

        // If one of the GameObject dies "too fast"(QueueFree() is called immidiately)
        // after LightningBulletPhysicsController.Update() is executed,
        // that GameObject becomes a null item in the lightningBullet.BodiesHit array
        // and it won't be able to get the global position of the GameObject
        if (o1 != null) {
            lightning.AddPoint(o1.GlobalPosition);
        }
        if (o2 != null) {
            lightning.AddPoint(o2.GlobalPosition);
        }
        node.GetParent().GetParent().AddChild(lightning);
        lightning.PlayAnimation();
    }
}
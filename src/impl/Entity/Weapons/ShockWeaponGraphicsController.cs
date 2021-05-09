using Godot;
using Godot.Collections;

public class ShockWeaponGraphicsController : WeaponGraphicsController
{
    PackedScene LightningScene = (PackedScene)GD.Load("res://Lightning.tscn");

    public void ChainingBodiesAnimation(Node2D node, Array bodiesHit)
    {
        if (bodiesHit.Count != 0)
        {
            ConnectLightning(node, node.GetNode<Node2D>("Sprite/Tip"), (Node2D)bodiesHit[0]);
        }

        for (int i = 0; i < bodiesHit.Count - 1; i++)
        {
            ConnectLightning(node, (Node2D)bodiesHit[i], (Node2D)bodiesHit[i + 1]);
        }
    }

    private void ConnectLightning(Node2D node, Node2D o1, Node2D o2)
    {
        var lightning = (Lightning)LightningScene.Instance();
        lightning.AddPoint(o1.GlobalPosition);
        lightning.AddPoint(o2.GlobalPosition);
        node.GetParent().GetParent().AddChild(lightning);
        lightning.PlayAnimation();
    }
}
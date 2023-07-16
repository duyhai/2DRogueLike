using System.Collections.Generic;
using Godot;

public partial class TeleportBlock : Block
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Map/Blocks/TeleportBlock.tscn");
    public TeleportBlock DestinationNode { get; set; }
    private Timer cooldownTimer;
    private List<Node2D> nodesOnCooldown = new List<Node2D>();

    public TeleportBlock() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    { }

    public override void _Ready()
    {
        CollisionMask = CollisionLayers.Player | CollisionLayers.Enemy;
        Area2D area = GetNode<Area2D>("Area2D");
        area.CollisionLayer = CollisionLayer;
        area.CollisionMask = CollisionMask;

        CollisionLayer = 0;
        string group = GetGroups()[0];  // TeleportBlock can only have one group

        foreach (var node in GetTree().GetNodesInGroup(group))
        {
            if (node != this && node is TeleportBlock block)
            {
                DestinationNode = block;
                DestinationNode.DestinationNode = this;
                break;
            }
        }
    }

    public override int Hit(int damage)
    {
        return 0;
    }

    public void OnArea2DBodyEntered(Node2D body)
    {
        if (nodesOnCooldown.Contains(body))
        {
            return;
        }

        DestinationNode.TeleportNode(body);
    }

    public void OnArea2DBodyExited(Node2D body)
    {
        nodesOnCooldown.Remove(body);
    }

    public void TeleportNode(Node2D body)
    {

        body.GlobalPosition = this.GlobalPosition;
        nodesOnCooldown.Add(body);

        if (body is Player player)
        {
            TeleportCameraEffect cameraEffect = TeleportCameraEffect.SceneObject.Instantiate<TeleportCameraEffect>();
            player.PlayCameraEffect(cameraEffect);
        }
    }
}

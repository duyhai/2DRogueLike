using Godot;
using System.Linq;

public partial class Enemy : GameObject
{
    public Enemy(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    {

    }

    public override void _Ready()
    {
        base._Ready();
        AddToGroup(NodeGroups.Enemy);

        Area2D sight = GetNodeOrNull<Area2D>("Sight");
        if (sight == null) return;

        sight.CollisionLayer = CollisionLayer;
        sight.CollisionMask = CollisionMask;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        SightCheck();
    }

    public Player SightCheck()
    {
        Area2D sight = GetNodeOrNull<Area2D>("Sight");
        if (sight == null) return null;

        var overlappingBodies = sight.GetOverlappingBodies();
        Player nearestPlayer = null;
        var spaceState = GetWorld2D().DirectSpaceState;
        var playerNodes = GetTree().GetNodesInGroup(NodeGroups.Player);
        foreach (Player player in playerNodes.Cast<Player>())
        {
            if (!overlappingBodies.Contains(player))
            {
                continue;
            }

            var sightCheck = spaceState.IntersectRay(new PhysicsRayQueryParameters2D()
            {
                From = GlobalPosition,
                To = player.GlobalPosition,
                Exclude = new Godot.Collections.Array<Rid> { this.GetRid() },
                CollisionMask = CollisionMask
            });

            if (sightCheck.ContainsKey("rid") && player.GetRid() == (Rid)sightCheck["rid"])
            {
                if (nearestPlayer == null || Position.DistanceTo(player.Position) < Position.DistanceTo(nearestPlayer.Position))
                {
                    nearestPlayer = player;
                }
            }
        }
        return nearestPlayer;
    }
}
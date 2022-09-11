using Godot;
using System.Linq;

public class Enemy : GameObject
{
    public Enemy(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    {

    }

    public override void _Ready()
    {
        base._Ready();
        AddToGroup(NodeGroups.Enemy);
    }

    public override void _Process(float delta)
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
        var spaceState = GetWorld2d().DirectSpaceState;
        var playerNodes = GetTree().GetNodesInGroup(NodeGroups.Player);
        foreach (Player player in playerNodes)
        {
            if (!overlappingBodies.Contains(player)) continue;

            var sightCheck = spaceState.IntersectRay(Position, player.Position, new Godot.Collections.Array { this }, CollisionMask);

            if (sightCheck.Contains("collider") && player == sightCheck["collider"])
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
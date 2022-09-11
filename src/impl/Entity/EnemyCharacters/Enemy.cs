using Godot;

public class Enemy : GameObject
{
    public bool PlayerInSight { get; set; }
    private bool playerInRange;
    private Player player;

    public Enemy(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    {

    }

    public override void _Ready()
    {
        base._Ready();
        AddToGroup("enemy");
        System.Console.WriteLine("asd");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        SightCheck();
        if (player == null)
        {
            player = GetParent().GetNode<Player>("Player");
        }
    }

    public void OnSightBodyEntered(Node body)
    {
        if (body == player)
        {
            playerInRange = true;
        }
    }

    public void OnSightBodyExited(Node body)
    {
        if (body == player)
        {
            playerInRange = false;
        }
    }

    public void SightCheck()
    {
        PlayerInSight = false;
        if (playerInRange)
        {
            var spaceState = GetWorld2d().DirectSpaceState;
            var sightCheck = spaceState.IntersectRay(Position, player.Position, new Godot.Collections.Array { this }, CollisionMask);
            PlayerInSight = sightCheck.Contains("collider") && sightCheck["collider"] == player;
        }
    }
}
using Godot;

public partial class PremadeMap : Map
{
    private PackedScene mapScene;

    public PremadeMap(PackedScene scene, Node parentNode)
    {
        this.parentNode = parentNode;
        this.mapScene = scene;
    }

    public override void Instance()
    {
        Node mapNode = mapScene.Instantiate();
        var mapNodePlayerInstance = mapNode.GetNode<Player>("Player");
        PlayerSpawn = mapNodePlayerInstance.Position;

        foreach (Node node in mapNode.GetChildren())
        {
            if (node.Name == "Player")
                continue;

            mapNode.RemoveChild(node);
            parentNode.AddChild(node);
        }

        var playerInstance = parentNode.GetNodeOrNull<Player>("Player");
        if (playerInstance == null)
        {
            mapNode.RemoveChild(mapNodePlayerInstance);
            parentNode.AddChild(mapNodePlayerInstance);
            playerInstance = mapNodePlayerInstance;
        }
        playerInstance.Connect("DeathSignal", new Callable(parentNode.GetParent(), "OnPlayerDeathSignal"));
    }
}
using Godot;

public class FCTManager : Node2D
{
    public static FCTManager Instance { get; set; }
    private readonly PackedScene floatingTextScene = (PackedScene)GD.Load("FloatingText.tscn");
    private readonly Vector2 travel = new Vector2(0, -10);
    private readonly float duration = 0.3f;
    private readonly int spread = 60;
    public Node TextContainer { get; set; }

    public void ShowValue(string value, Vector2 position, Color color)
    {
        var floatingTextInstance = (FloatingText)floatingTextScene.Instance();
        floatingTextInstance.Position = position;
        floatingTextInstance.Modulate = color;
        TextContainer.AddChild(floatingTextInstance);
        floatingTextInstance.ShowValue(value, travel, duration, spread);
    }
    public override void _Ready()
    {
        Instance = this;
    }
}

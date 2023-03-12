using Godot;

public class MainMenu : MarginContainer
{
    public override void _Ready()
    {
        base._Ready();

        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

    public override void _ExitTree()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;
        base._ExitTree();
    }
}

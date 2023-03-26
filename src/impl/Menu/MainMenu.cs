using Godot;

public partial class MainMenu : MarginContainer
{
	public override void _Ready()
	{
		base._Ready();

		Input.MouseMode = Input.MouseModeEnum.Visible;
		GD.Print("Menu");
	}

	public override void _ExitTree()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		base._ExitTree();
	}
}

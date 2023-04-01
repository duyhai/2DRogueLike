using Godot;
using System;

public partial class Options : VBoxContainer
{
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

    public override void _ExitTree()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;
        base._ExitTree();
    }
}

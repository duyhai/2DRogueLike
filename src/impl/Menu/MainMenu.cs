using Godot;
using System;

public class MainMenu : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (var i in GetNode("HBoxContainer/VBoxContainer/MenuOptions").GetChildren())
        {
            var button = (MenuButton)i;
            button.Connect("pressed", this, nameof(OnButtonPressed), new Godot.Collections.Array() { button.sceneToLoad });
        }
    }

    public void OnButtonPressed(string sceneToLoad)
    {
        GetTree().ChangeScene(sceneToLoad);
    }   
}

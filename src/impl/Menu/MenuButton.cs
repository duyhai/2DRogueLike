using Godot;
using System;

public class MenuButton : Button
{
    [Export]
    public string sceneToLoad;

    public override void _Ready()
    {
        Connect("pressed", this, nameof(OnButtonPressed), new Godot.Collections.Array() { sceneToLoad });
    }

    public void OnButtonPressed(string sceneToLoad)
    {
        var sceneManager = (SceneManager)GetNode("/root/SceneManager");
        sceneManager.GotoScene(sceneToLoad);
    } 
}

using System;
using Godot;

public partial class MenuButton : Button
{
    [Export]
    public string sceneToLoad;

    public override void _Ready()
    {
        // Connect("pressed", Callable.From(action))/*,new Godot.Collections.Array() { sceneToLoad }*/;
        // Connect("pressed", Callable.From(action));
        Pressed += OnButtonPressed;
        GD.Print(sceneToLoad);
    }

    public void OnButtonPressed()
    {
        GD.Print(sceneToLoad);
        if (sceneToLoad == Main.SCENE_PATH)
        {
            SceneManager.Instance.ClearPausedScene();
        }

        if (sceneToLoad == "Continue")
        {
            SceneManager.Instance.GotoPausedScene();
        }
        else
        {
            SceneManager.Instance.GotoScene(sceneToLoad);
        }
    }
}

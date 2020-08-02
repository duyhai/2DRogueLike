using Godot;
using System;

public class SceneManager : Godot.Node
{
    public Node CurrentScene { get; set; }
    public Node PausedScene { get; set; }
    public static SceneManager Instance { get; set; }

    public override void _Ready()
    {
        Instance = this;
        Viewport root = GetTree().Root;
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
    }
    public void GotoScene(string path, bool paused = false)
    {
        // This function will usually be called from a signal callback,
        // or some other function from the current scene.
        // Deleting the current scene at this point is
        // a bad idea, because it may still be executing code.
        // This will result in a crash or unexpected behavior.

        // The solution is to defer the load to a later time, when
        // we can be sure that no code from the current scene is running:

        CallDeferred(nameof(DeferredGotoScene), path, paused);
    }
    
    public void GotoPausedScene()
    {
        CallDeferred(nameof(DeferredGotoPausedScene));
    }

    public void DeferredGotoScene(string path, bool doPause)
    {
        if (doPause)
        {
            ClearPausedScene();
            PausedScene = CurrentScene;
            GetTree().Paused = true;
            GetTree().Root.RemoveChild(PausedScene);
        }
        else
        {
            // It is now safe to remove the current scene
            CurrentScene.Free();
        }

        // Load a new scene.
        var nextScene = (PackedScene)GD.Load(path);

        // Instance the new scene.
        CurrentScene = nextScene.Instance();

        // Add it to the active scene, as child of root.
        GetTree().Root.AddChild(CurrentScene);

        // Optionally, to make it compatible with the SceneTree.change_scene() API.
        GetTree().CurrentScene = CurrentScene;
    }

    public void DeferredGotoPausedScene()
    {
        if (PausedScene == null) return;

        GetTree().Root.AddChild(PausedScene);
        GetTree().Paused = false;
        CurrentScene.Free();
        CurrentScene = PausedScene;
        PausedScene = null;
        GetTree().CurrentScene = CurrentScene;
    }

    public void ClearPausedScene()
    {
        if (PausedScene == null) return;

        PausedScene.Free();
        PausedScene = null;
        GetTree().Paused = false;
    }
}
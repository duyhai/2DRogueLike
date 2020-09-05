using Godot;

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

using Godot;

public class FlamethrowerFlameGraphicsController : GraphicsController
{
    public override void Update(GameObject gameObject)
    {

    }

    public void PlayFadeAnimation(GameObject gameObject)
    {
        AnimationPlayer animPlayer = gameObject.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("Flame");
    }
}
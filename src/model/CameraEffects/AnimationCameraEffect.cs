using Godot;

public class AnimationCameraEffect : Sprite, ICameraEffect
{
    public bool IsEnded
    {
        get;
        protected set;
    }

    public void Update(Camera2D camera, float delta) { }

    public void Start(Camera2D camera)
    {
        camera.AddChild(this);
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Fade");
    }

    public void OnAnimationPlayerAnimationFinished(string animName)
    {
        IsEnded = true;
        QueueFree();
    }
}
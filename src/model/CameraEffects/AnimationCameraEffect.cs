using Godot;

public partial class AnimationCameraEffect : Sprite2D, ICameraEffect
{
    public bool IsEnded
    {
        get;
        protected set;
    }

    public void Update(Camera2D camera, double delta) { }

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
using Godot;
using System;

public class HitCameraEffect : Sprite, ICameraEffect
{
    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/CameraEffectNodes/HitCameraEffect.tscn");

    public bool IsEnded
    {
        get;
        private set;
    }

    public void Start(Camera2D camera)
    {
        camera.AddChild(this);
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Fade");
    }

    public void Update(Camera2D camera, float delta) { }

    public void OnAnimationPlayerAnimationFinished(string animName)
    {
        IsEnded = true;
        QueueFree();
    }
}
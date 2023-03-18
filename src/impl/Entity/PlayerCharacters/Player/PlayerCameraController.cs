using Godot;
using System.Collections.Generic;

public class PlayerCameraController : CameraController
{
    List<ICameraEffect> cameraEffects = new List<ICameraEffect>();

    public PlayerCameraController(Camera2D camera) : base(camera) { }

    public override void Update(GameObject gameObject, float delta)
    {
        foreach (var effect in cameraEffects)
        {
            effect.Update(camera, delta);
        }
        cameraEffects.RemoveAll(i => i.IsEnded);
    }

    public void AddCameraAffect(ICameraEffect cameraEffect)
    {
        cameraEffects.Add(cameraEffect);
    }
}
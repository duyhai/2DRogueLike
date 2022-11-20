using Godot;
using System.Collections.Generic;

public class PlayerCameraController : CameraController
{
    List<CameraEffect> cameraEffects = new List<CameraEffect>();

    public PlayerCameraController(Camera2D camera) : base(camera) { }

    public override void Update(GameObject gameObject, float delta)
    {
        foreach (var effect in cameraEffects)
        {
            effect.Update(camera, delta);
        }
    }

    public void AddCameraAffect(CameraEffect cameraEffect)
    {
        cameraEffects.Add(cameraEffect);
    }
}
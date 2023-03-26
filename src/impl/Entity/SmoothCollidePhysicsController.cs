using Godot;
using System;

public partial class SmoothCollidePhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        gameObject.MoveAndSlide();
    }
}
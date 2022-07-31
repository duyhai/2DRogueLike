using Godot;
using System;

public class SmoothCollidePhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        gameObject.MoveAndSlide(gameObject.velocity);
    }
}
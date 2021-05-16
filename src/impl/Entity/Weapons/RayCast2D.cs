using Godot;
using System;

public class LaserBeam : Godot.RayCast2D
{
    bool isCasting = false;

    public override void _Ready()
    {
        SetPhysicsProcess(false);
        GetNode<Line2D>("Line2D").Points[1] = Vector2.Zero;
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 castPoint = CastTo;
        ForceRaycastUpdate();

        if (IsColliding())
        {
            castPoint = ToLocal(GetCollisionPoint());
        }
        GetNode<Line2D>("Line2D").SetPointPosition(1, castPoint);
    }

    void setIsCasting(bool cast)
    {
        isCasting = cast;

        if (isCasting)
        {
            appear();
        }
        else
        {
            disappear();
        }

        SetPhysicsProcess(isCasting);
    }

    void appear()
    {
        Tween tween = GetNode<Tween>("Tween");
        tween.StopAll();
        tween.InterpolateProperty(GetNode<Line2D>("Line2D"), "width", 0, 10.0f, 0.2f);
        tween.Start();
    }

    void disappear()
    {
        Tween tween = GetNode<Tween>("Tween");
        tween.StopAll();
        tween.InterpolateProperty(GetNode<Line2D>("Line2D"), "width", 10.0, 0, 0.1f);
        tween.Start();
    }
}

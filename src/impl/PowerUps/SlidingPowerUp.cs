using Godot;
using System;

public partial class SlidingPowerUp : PowerUp
{
    Vector2 gameObjectVelocity;

    public override void Initiate(GameObject target, GameObject initiator)
    {
        base.Initiate(target, initiator);
        gameObjectVelocity = target.Velocity;
        if (target.Velocity.Length() > 0.01f)
        {
            target.Sliding = true;
        }
        target.Velocity = gameObjectVelocity;
    }

    public override void UndoEffect()
    {
        GetParent<GameObject>().Sliding = false;
        GetParent<GameObject>().Velocity = gameObjectVelocity;
        base.UndoEffect();
    }
}

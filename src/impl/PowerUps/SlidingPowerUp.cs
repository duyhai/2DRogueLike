using Godot;
using System;

public partial class SlidingPowerUp : PowerUp
{
    Vector2 gameObjectVelocity;

    public override void Initiate(GameObject target, GameObject initiator)
    {
        base.Initiate(target, initiator);
        gameObjectVelocity = target.velocity;
        if (target.velocity.Length() > 0.01f)
        {
            target.Sliding = true;
        }
        target.velocity = gameObjectVelocity;
    }

    public override void UndoEffect()
    {
        GetParent<GameObject>().Sliding = false;
        GetParent<GameObject>().velocity = gameObjectVelocity;
        base.UndoEffect();
    }
}

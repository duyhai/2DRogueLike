using Godot;
using System;

public class SlidingPowerUp : PowerUp
{
    Vector2 gameObjectVelocity;

    public override void Initiate(GameObject target, GameObject initiator)
    {
        base.Initiate(target, initiator);
        gameObjectVelocity = target.velocity;
    }

    public override void Effect()
    {
        GetParent<GameObject>().Sliding = true;
        GetParent<GameObject>().velocity = gameObjectVelocity;
    }

    public override void UndoEffect()
    {
        GetParent<GameObject>().Sliding = false;
        GetParent<GameObject>().velocity = gameObjectVelocity;
        base.UndoEffect();
    }
}

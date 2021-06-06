using Godot;
using System;

public class SpeedPowerUp : PowerUp
{
    float modifier = 6f;

    public override void _Ready()
    {
        Effect();
        base._Ready();
    }

    public override void Effect()
    {
        GetParent<GameObject>().speedModifier += modifier;
    }

    public override void UndoEffect()
    {
        GetParent<GameObject>().speedModifier -= modifier;
        base.UndoEffect();
    }
}

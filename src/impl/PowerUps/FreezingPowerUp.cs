using Godot;
using System;

public class FreezingPowerUp : MovSpeedModPowerUp
{
    int MAX_STACK_COUNT = 50;
    public int StackCount = 0;
    public FreezingPowerUp()
    {
        modifier = 0.5f;
    }

    public override void Reset()
    {
        if (StackCount < MAX_STACK_COUNT)
        {
            StackCount++;
            base.Reset();
        }
        else
        {
            modifier = 0;
        }
    }

    public override void Effect()
    {
        GetParent<GameObject>().DisableInput = true;
    }

    public override void UndoEffect()
    {
        GetParent<GameObject>().DisableInput = false;
        base.UndoEffect();
    }
}

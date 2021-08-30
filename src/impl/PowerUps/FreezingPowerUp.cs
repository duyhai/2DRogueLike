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
        if (MAX_STACK_COUNT == StackCount)
        {
            GetParent<GameObject>().DisableInput = true;
            modifier = 0;
            GetNode<Particles2D>("Particles2D").Modulate = new Color("00dacf");
        }
    }

    public override void UndoEffect()
    {
        base.UndoEffect();
        GetParent<GameObject>().DisableInput = false;
    }
}

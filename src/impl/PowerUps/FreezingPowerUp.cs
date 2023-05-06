using Godot;
using System;

public partial class FreezingPowerUp : MovSpeedModPowerUp
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
            modifier = 0;
            GetParent<GameObject>().Velocity = Vector2.Zero;
            GetParent<GameObject>().DisableInput = true;
            GetNode<GpuParticles2D>("GPUParticles2D").Modulate = new Color("00dacf");
        }
    }

    public override void UndoEffect()
    {
        base.UndoEffect();
        GetParent<GameObject>().DisableInput = false;
    }
}

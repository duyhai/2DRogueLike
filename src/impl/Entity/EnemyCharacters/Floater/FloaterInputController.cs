using Godot;
using System;

public partial class FloaterInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        Floater floater = (Floater)gameObject;

        if (floater.isDead)
        {
            floater.Velocity = Vector2.Zero;
            return;
        }

        Random rnd = new Random();
        StatsInfo floaterStats = floater.Stats;
        floater.Velocity = new Vector2(rnd.Next(-floaterStats.Speed, floaterStats.Speed), rnd.Next(-floaterStats.Speed, floaterStats.Speed));

        floater.Velocity = floater.Velocity.Normalized() * floaterStats.Speed;
    }
}
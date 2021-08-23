using Godot;
using System;

public class FloaterInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        Floater floater = (Floater)gameObject;

        if (floater.isDead)
        {
            floater.velocity = Vector2.Zero;
            return;
        }

        Random rnd = new Random();
        StatsInfo floaterStats = floater.Stats;
        floater.velocity = new Vector2(rnd.Next(-floaterStats.Speed, floaterStats.Speed), rnd.Next(-floaterStats.Speed, floaterStats.Speed));

        floater.velocity = floater.velocity.Normalized() * floaterStats.Speed;
    }
}
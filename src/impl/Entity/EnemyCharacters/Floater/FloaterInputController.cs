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
        floater.velocity = new Vector2(rnd.Next(-floater.Stats.Speed, floater.Stats.Speed), rnd.Next(-floater.Stats.Speed, floater.Stats.Speed));

        floater.velocity = floater.velocity.Normalized() * floater.Stats.Speed;
    }
}
using Godot;
using System;

public class FloaterInputController : InputController 
{
    public override void Update(GameObject gameObject)
    {        
        Floater floater = (Floater)gameObject;
	    Random rnd = new Random();
        floater.velocity = new Vector2(rnd.Next(-floater.speed, floater.speed), rnd.Next(-floater.speed, floater.speed));

        floater.velocity = floater.velocity.Normalized() * floater.speed;
    }
}
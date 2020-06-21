using Godot;
using System;

public class SimpleBullet : Bullet
{
    public SimpleBullet()
    {
        speed = 750;
        physicsController = new SimpleBulletPhysicsController();
    }
}

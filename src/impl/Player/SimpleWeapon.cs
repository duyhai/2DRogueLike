using Godot;
using System;

public class SimpleWeapon : Weapon
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weaponCooldown = 0.25f;
        bulletTimer = GetNode<Timer>("BulletTimer");
        bulletScene = (PackedScene)GD.Load("res://Bullet.tscn");
        bulletTimer.WaitTime = weaponCooldown;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

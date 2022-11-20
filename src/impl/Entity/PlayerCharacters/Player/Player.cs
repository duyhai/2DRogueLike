using Godot;
using System;
using System.Collections.Generic;

public class Player : GameObject
{
    public event Action<Weapon> WeaponChanged;
    public event Action<List<Weapon>> WeaponListChanged;
    public List<Weapon> weapons = new List<Weapon>();
    public int equippedWeaponIndex = 0;

    private CameraController cameraController;

    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Player.tscn");

    public Player() :
        base(new PlayerInputController(), new SmoothCollidePhysicsController(), new PlayerGraphicsController())
    {
        baseStats = new StatsInfo { MaxHealth = 2000, MaxShield = 0, Damage = 100, Speed = 200 };
        health = Stats.MaxHealth;
        CollisionLayer = CollisionLayers.Player;
        CollisionMask = CollisionLayers.Enemy | CollisionLayers.Wall | CollisionLayers.Water;
    }

    public override void _Ready()
    {
        weapons.Add((Weapon)GetNode("SimpleWeapon"));
        weapons.Add((Weapon)GetNode("BouncyBulletWeapon"));
        weapons.Add((Weapon)GetNode("Flamethrower"));
        weapons.Add((Weapon)GetNode("RocketLauncher"));
        weapons.Add((Weapon)GetNode("ShockWeapon"));
        weapons.Add((Weapon)GetNode("Melee"));
        weapons.Add((Weapon)GetNode("LaserWeapon"));
        weapons.Add((Weapon)GetNode("BallLightningWeaponV2"));
        weapons.Add((Weapon)GetNode("FreezingWeapon"));
        weapons[equippedWeaponIndex].Visible = true;

        cameraController = new PlayerCameraController(GetNode<Camera2D>("Camera2D"));
        ((PlayerGraphicsController)graphicsController).CameraController = cameraController;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        cameraController?.Update(this, delta);
    }

    public void Shoot(Vector2 vector)
    {
        weapons[equippedWeaponIndex].Shoot(vector);
    }

    public void Respawn(float x, float y)
    {
        ResetHealth();
        DisableInput = false;
        Position = new Vector2(x, y);
        ((BasicGraphicsController)graphicsController).ResetSprite(this);
    }

    public override void OnAnimationFinished(string animationName) { }

    public void PreviousWeapon()
    {
        weapons[equippedWeaponIndex--].Visible = false;
        equippedWeaponIndex = (equippedWeaponIndex + weapons.Count) % weapons.Count;
        weapons[equippedWeaponIndex].Visible = true;
        WeaponChanged?.Invoke(weapons[equippedWeaponIndex]);
    }

    public void NextWeapon()
    {
        weapons[equippedWeaponIndex++].Visible = false;
        equippedWeaponIndex = (equippedWeaponIndex + weapons.Count) % weapons.Count;
        weapons[equippedWeaponIndex].Visible = true;
        WeaponChanged?.Invoke(weapons[equippedWeaponIndex]);
    }
}

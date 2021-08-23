using Godot;
using System.Collections.Generic;

public class Player : GameObject
{
    public List<Weapon> weapons = new List<Weapon>();
    public int equippedWeaponIndex = 0;

    public static PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/Entity/Player.tscn");

    public Player() :
        base(new PlayerInputController(), new SmoothCollidePhysicsController(), new BasicGraphicsController())
    {
        this.baseSpeed = 200;
        this.maxHealth = 100;
        this.health = maxHealth;
        CollisionLayer = CollisionLayers.Player;
        CollisionMask = CollisionLayers.Enemy | CollisionLayers.MapObject;
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
        weapons.Add((Weapon)GetNode("BallLightningWeapon"));
        weapons[equippedWeaponIndex].Visible = true;
    }

    public void Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        weapons[equippedWeaponIndex].Shoot(vector, collisionLayer, collisionMask);
    }

    public void Respawn(float x, float y)
    {
        ResetHealth();
        disableInput = false;
        Position = new Vector2(x, y);
        ((BasicGraphicsController)graphicsController).ResetSprite(this);
    }

    public override void OnAnimationFinished(string animationName) { }
}

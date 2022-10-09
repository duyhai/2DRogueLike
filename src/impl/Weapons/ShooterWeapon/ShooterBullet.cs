using Godot;

public class ShooterBullet : SimpleBullet
{
    public static new PackedScene SceneObject = (PackedScene)GD.Load("res://scenes/weapons/projectiles/ShooterBullet.tscn");

    public ShooterBullet()
    {
        baseStats = new StatsInfo { MaxHealth = 0, MaxShield = 0, Damage = 0, Speed = 300 };
    }
}
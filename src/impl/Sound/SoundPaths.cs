using System;

public static class SoundPaths
{
    public static string Explosion = "res://assets/weapons/explosion.wav";
    public static string Gunshot = "res://assets/weapons/gunshot.wav";
    public static string Laser = "res://assets/weapons/laser.wav";
    public static string RocketLaunching = "res://assets/weapons/rocket_launching.wav";
    public static string Lightning = "res://assets/weapons/lightning.wav";
    public static string Bounce = "res://assets/weapons/bounce.wav";
    public static string BouncyWeapon = "res://assets/weapons/bouncyweapon.wav";
    public static string LaserBeam = "res://assets/weapons/laserbeam.wav";
    public static string LaserBeamOGG = "res://assets/weapons/laserbeam.ogg";
    public static string PlayerHit
    {
        get
        {
            Random rnd = new Random();
            return $"res://assets/player/hit{rnd.Next(1, 5)}.wav";
        }
    }
    public static string PlayerDeath = "res://assets/player/death.wav";
}
using Godot;
using System;

public partial class FreezingBulletGraphicsController : GraphicsController
{
    int NUMBER_OF_TEXTURES = 3;

    public override void Update(Node2D node)
    {

    }

    public void RandomTexture(FlamethrowerFlame flame)
    {
        Random rnd = new Random();
        int textureNumber = rnd.Next(1, NUMBER_OF_TEXTURES + 1);
        var texture = ResourceLoader.Load("res://assets/weapons/snow" + textureNumber + ".png");
        Sprite2D sprite = flame.GetNode<Sprite2D>("Sprite2D");
        sprite.Texture = (Texture2D)texture;
    }

    public void PlayFadeAnimation(Node2D node)
    {
        AnimationPlayer animPlayer = node.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        animPlayer?.Play("Fade");
    }
}
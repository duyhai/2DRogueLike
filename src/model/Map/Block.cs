using System;
using Godot;

public abstract partial class Block : GameObject
{
    private Random rnd = new Random();
    protected Texture2D[] Textures { get; set; }

    public Block(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    { }

    public override void _Ready()
    {
        Sprite2D sprite = GetNode<Sprite2D>("Sprite2D");
        if (sprite != null && Textures != null && Textures.Length != 0)
        {
            sprite.Texture = Textures[rnd.Next(Textures.Length)];
            sprite.FlipH = rnd.Next(2) == 1;
        }

        CollisionLayer = CollisionLayers.Wall;
        CollisionMask = CollisionLayers.Player;
    }
}
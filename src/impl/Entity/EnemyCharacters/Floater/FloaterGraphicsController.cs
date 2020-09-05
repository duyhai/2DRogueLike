using Godot;

public class FloaterGraphicsController : GraphicsController 
{
    public override void Update(GameObject gameObject)
    {
        Floater floater = (Floater)gameObject;
        AnimatedSprite animSprite = gameObject.GetNode<AnimatedSprite>("AnimatedSprite");
        
        bool goingLeft = floater.velocity.x < 0;
        animSprite.FlipH = goingLeft;

        bool goingUp = floater.velocity.y < 0;
        animSprite.Animation = goingUp ? "walkUp" : "walk";

        bool moving = floater.velocity.Length() > 0.0001;
        animSprite.Playing = moving;
    }
}
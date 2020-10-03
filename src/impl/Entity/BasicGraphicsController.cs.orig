using Godot;

public class BasicGraphicsController : GraphicsController 
{
    public override void Update(GameObject gameObject)
    {
        AnimatedSprite animSprite = gameObject.GetNode<AnimatedSprite>("AnimatedSprite");
        
        bool goingLeft = gameObject.velocity.x < 0;
        animSprite.FlipH = goingLeft;

        bool goingUp = gameObject.velocity.y < 0;
        animSprite.Animation = goingUp ? "walkUp" : "walk";

        bool moving = gameObject.velocity.Length() > 0.0001;
        animSprite.Playing = moving;
    }
}
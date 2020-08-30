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
        if (goingUp) {
            animSprite.Animation = "walkUp";
        } else {
            animSprite.Animation = "walk";
        }

        bool moving = floater.velocity.Length() > 0.0001;
        animSprite.Playing = moving;

        if (floater.health <= 0)
        {
            floater.QueueFree();
        }
    }
}
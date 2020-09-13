using Godot;

public class PlayerGraphicsController : GraphicsController 
{
    public override void Update(GameObject gameObject)
    {
        Player player = (Player)gameObject;
        AnimatedSprite animSprite = gameObject.GetNode<AnimatedSprite>("AnimatedSprite");
        
        bool goingLeft = player.velocity.x < 0;
        animSprite.FlipH = goingLeft;

        bool goingUp = player.velocity.y < 0;
        animSprite.Animation = goingUp ? "walkUp" : "walk";

        bool moving = player.velocity.Length() > 0.0001;
        animSprite.Playing = moving;
    }
}
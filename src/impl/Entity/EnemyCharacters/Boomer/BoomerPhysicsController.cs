using Godot;

public class BoomerPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        Boomer boomer = (Boomer)gameObject;
        var collision = boomer.MoveAndCollide(boomer.velocity * delta);

        if (collision != null)
        {
            boomer.velocity = boomer.velocity.Slide(collision.Normal);

            Node collider = (Node)collision.Collider;
            if (typeof(Player).Equals(collider.GetType()))
            {
                boomer.Death();
            }
        }
    }
}
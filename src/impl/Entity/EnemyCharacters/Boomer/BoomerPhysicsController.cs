using Godot;

public partial class BoomerPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        Boomer boomer = (Boomer)gameObject;
        var collision = boomer.MoveAndCollide(boomer.velocity * (float)delta);

        if (collision != null)
        {
            boomer.velocity = boomer.velocity.Slide(collision.GetNormal());

            Node collider = (Node)collision.GetCollider();
            if (typeof(Player).Equals(collider.GetType()))
            {
                boomer.OnDeathFinished();
            }
        }
    }
}
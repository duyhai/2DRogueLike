public class BreakableWall : Block
{
    public BreakableWall() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }

    public override void Hit(int damage)
    {
        QueueFree();
    }
}

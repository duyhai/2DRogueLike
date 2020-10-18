public class BreakableWall : Block
{
<<<<<<< HEAD
	public BreakableWall() :
		base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController()) 
	{
		
	}
=======
    public BreakableWall() :
        base(new NullInputController(), new NullPhysicsController(), new NullGraphicsController())
    {

    }
>>>>>>> master

    public override void Hit(int damage)
    {
        QueueFree();
    }
}

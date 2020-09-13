public class BreakableWall : Block
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public override void Hit()
	{
		QueueFree();
	}
}

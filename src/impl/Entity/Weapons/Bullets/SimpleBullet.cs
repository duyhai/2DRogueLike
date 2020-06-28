

public class SimpleBullet : Bullet
{
    public SimpleBullet():
        base(new NullInputController(), new SimpleBulletPhysicsController(), new NullGraphicsController())
    {
        speed = 750;
    }
}

using Godot;

public abstract class Bullet : GameObject
{
    protected int damage;
    protected GameObject initiator;

    public Bullet(InputController inputController, PhysicsController physicsController, GraphicsController graphicsController) :
        base(inputController, physicsController, graphicsController)
    { }

    public void OnVisibilityNotifier2DScreenExited()
    {
        if (!GetTree().Paused)
        {
            QueueFree();
        }
    }

    public void Initiate(GameObject initiator, float rotation, Vector2 position, int damage)
    {
        Position = position;
        velocity = new Vector2(speed, 0).Rotated(rotation);
        Rotation = (Mathf.Pi / 2) + rotation;
        this.damage = damage;
        this.initiator = initiator;
    }

    public virtual int HitTarget(Node collider)
    {
        var method = collider.GetType().GetMethod("Hit");
        int inflictedDamage = (int)method?.Invoke(collider, new object[] { damage });

        if (inflictedDamage > 0)
        {
            float lifestealPercentage = 0f;
            if (initiator.IsInsideTree())
            {
                var powerUps = GroupUtils.FindNodeDescendantsInGroup(initiator, "LifestealPowerUp");
                for (int i = 0; i < powerUps.Count; i++)
                {
                    LifestealPowerUp lifestealPowerUp = (LifestealPowerUp)powerUps[i];
                    lifestealPercentage += lifestealPowerUp.Percentage;
                }
            }
            initiator.Hit((int)(-inflictedDamage * lifestealPercentage));
        }
        return inflictedDamage;
    }

    public override int Hit(int damage) { return 0; }
}

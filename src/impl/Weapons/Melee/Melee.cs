using Godot;

public class Melee : Weapon
{
    public Melee() : base(new MeleeGraphicsController(), 25) { }
    Area2D swingingArea;

    public override void _Ready()
    {
        bulletTimer = GetNode<Timer>("BulletTimer");
        swingingArea = GetNode<Area2D>("Area2D");
        swingingArea.CollisionLayer = GetParent<GameObject>().CollisionLayer;
        swingingArea.CollisionMask = GetParent<GameObject>().CollisionMask;
    }

    public override bool Shoot(Vector2 vector, uint collisionLayer, uint collisionMask)
    {
        if (!bulletTimer.IsStopped())
        {
            return false;
        }
        bulletTimer.Start();
        var bodies = swingingArea.GetOverlappingBodies();

        int inflictedDamage = 0;
        float lifestealPercentage = 0f;
        GameObject initiator = GetParent<GameObject>();
        if (initiator.IsInsideTree())
        {
            var powerUps = GroupUtils.FindNodeDescendantsInGroup(initiator, "LifestealPowerUp");
            for (int i = 0; i < powerUps.Count; i++)
            {
                LifestealPowerUp lifestealPowerUp = (LifestealPowerUp)powerUps[i];
                lifestealPercentage += lifestealPowerUp.Percentage;
            }
        }

        foreach (var body in bodies)
        {
            var method = body.GetType().GetMethod("Hit");
            inflictedDamage += (int)method?.Invoke(body, new object[] { damage });
        }
        initiator.Hit((int)(-inflictedDamage * lifestealPercentage));

        ((MeleeGraphicsController)graphicsController).Swing(this);

        return true;
    }
}
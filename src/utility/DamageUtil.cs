using Godot;

public static class DamageUtil
{
    public static int HandleDamage(GameObject initiator, Node target, int damage)
    {
        var method = target.GetType().GetMethod("Hit");
        int inflictedDamage = (int)method?.Invoke(target, new object[] { damage });

        if (inflictedDamage > 0)
        {
            if (Object.IsInstanceValid(initiator) && initiator.IsInsideTree())
            {
                var powerUps = GroupUtils.FindNodeDescendantsInGroup(initiator, "PowerUp");
                for (int i = 0; i < powerUps.Count; i++)
                {
                    PowerUp powerUp = (PowerUp)powerUps[i];
                    powerUp.DamageEffect(initiator, target, inflictedDamage);
                }
            }
        }

        return inflictedDamage;
    }
}
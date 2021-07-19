using Godot;

public static class DamageUtil
{
    public static int HandleDamage(GameObject initiator, Node target, int damage)
    {
        var method = target.GetType().GetMethod("Hit");
        int inflictedDamage = (int)method?.Invoke(target, new object[] { damage });

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
            int lifesteal = (int)(-inflictedDamage * lifestealPercentage);
            if (lifesteal != 0)
            {
                initiator.Hit(lifesteal);
            }
        }

        return inflictedDamage;
    }
}
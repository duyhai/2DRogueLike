using Godot;

public static class DamageUtil
{
    public static int HandleDamage(GameObject initiator, Node target, int damage)
    {
        var method = target.GetType().GetMethod("Hit");
        int inflictedDamage = (int)method?.Invoke(target, new object[] { damage });

        if (inflictedDamage > 0)
        {
            StatsInfo stats = new StatsInfo(0, 0, 0, 0);
            if (initiator.IsInsideTree())
            {
                var powerUps = GroupUtils.FindNodeDescendantsInGroup(initiator, "LifestealPowerUp");
                for (int i = 0; i < powerUps.Count; i++)
                {
                    LifestealPowerUp lifestealPowerUp = (LifestealPowerUp)powerUps[i];
                    stats = lifestealPowerUp.UpdateStats(stats);
                }
            }
            int lifesteal = (int)(-inflictedDamage * stats.LifeSteal);
            if (lifesteal != 0)
            {
                initiator.Hit(lifesteal);
            }
        }

        return inflictedDamage;
    }
}
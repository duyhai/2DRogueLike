public class ShieldPowerUp : PowerUp
{
    protected int absorptionAmount;

    public int AbsorbDamage(int damage)
    {
        int remainingDamage = damage - absorptionAmount;
        if (remainingDamage < 0)
        {
            absorptionAmount = -remainingDamage;
        }
        else
        {
            QueueFree();
        }
        return remainingDamage;
    }
}
public partial class StatsInfo
{
    public int MaxHealth;
    public int MaxShield;
    public int Damage;
    public int Speed;

    public StatsInfo() { }

    public StatsInfo(int maxHealth, int maxShield, int damage, int speed)
    {
        MaxHealth = maxHealth;
        MaxShield = maxShield;
        Damage = damage;
        Speed = speed;
    }
}
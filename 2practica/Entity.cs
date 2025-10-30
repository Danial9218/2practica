abstract class Entity : IDamageDealer
{
    protected double _baseDamage = 10.0;
    protected double _health;
    protected double _maxHealth;

    public Entity(string name, double maxHealth)
    {
        Name = name;
        _maxHealth = maxHealth;
        Health = maxHealth;
    }

    public string Name { get; }
    public double MaxHealth => _maxHealth;

    public double Health
    {
        get => _health;
        set => _health = Math.Max(0, value);
    }

    public bool isDead => Health <= 0;

    public abstract double DoSomeDmg(double modifier);

    public virtual void TakeDamage(double damage)
    {
        Health = Math.Max(0, Health - damage);
    }

    public override string ToString()
    {
        return $"{Name} ({Health:F2} / {MaxHealth:F2})";
    }
}
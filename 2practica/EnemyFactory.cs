static class EnemyFactory
{
    public static Enemy Create(string name)
    {
        var enemy = new Enemy(name, maxHealth: Random.Shared.Next(120, 225))
        {
            CD = Random.Shared.Next(8, 15),
            EvadeChance = Random.Shared.Next(5, 20)
        };
        
        // Случайный уровень для врага
        for (int i = 0; i < Random.Shared.Next(1, 4); i++)
        {
            enemy.GainExperience(50); // Автоматически повышаем уровень
        }
        
        return enemy;
    }
}
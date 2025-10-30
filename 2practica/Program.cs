﻿var entityList = new List<Enemy>();

for (var i = 0; i < 5; i++)
{
    entityList.Add(EnemyFactory.Create(name: "Wolf"));
}

var player = new Player(name: "Roma", maxHealth: 200);

var dice = 0;
var enemyId = 0;
var round = 1;

while (true)
{
    if (player.isDead)
    {
        Console.WriteLine($"игра окончена! {player.Name} погиб на уровне {player.Level}");
        break;
    }

    // Проверяем победу
    if (entityList.All(x => x.isDead))
    {
        Console.WriteLine($"игра окончена! {player.Name} победил на уровне {player.Level}");
        break;
    }

    // Пропускаем мертвых врагов
    while (enemyId < entityList.Count && entityList[enemyId].isDead)
    {
        enemyId++;
    }

    // Если все враги мертвы - победа
    if (enemyId >= entityList.Count)
    {
        Console.WriteLine($"игра окончена! {player.Name} победил на уровне {player.Level}");
        break;
    }

    var currentEnemy = entityList[enemyId];

    Console.WriteLine($"\n--- Раунд: {round} ---");
    Console.WriteLine($"{player} (Ур.{player.Level}) vs {currentEnemy} (Ур.{currentEnemy.Level})");

    // Ход игрока
    dice = Random.Shared.Next(0, 20) + Random.Shared.Next(0, 20);

    if (dice > currentEnemy.CD)
    {
        dice = Random.Shared.Next(0, 100);
        if (dice > currentEnemy.EvadeChance)
        {
            double damage = player.DoSomeDmg(1.5);
            currentEnemy.TakeDamage(damage);
            Console.WriteLine($"{currentEnemy.Name} получил урон от {player.Name} ({dice}) - урон: {damage:F2}");
        }
        else
        {
            double damage = player.DoSomeDmg(2);
            currentEnemy.TakeDamage(damage);
            Console.WriteLine($"{currentEnemy.Name} увернулся от атаки, но получил урон ({dice}) - урон: {damage:F2}");
        }
    }
    else
    {
        Console.WriteLine($"{player.Name} неудачно атакует {currentEnemy.Name} ({dice})");
    }

    // Проверяем, не умер ли враг после атаки игрока
    if (currentEnemy.isDead)
    {
        double expGained = currentEnemy.Level * 25;
        player.GainExperience(expGained);
        
        Console.WriteLine($"\n враг {currentEnemy.Name} (Ур.{currentEnemy.Level}) побеждён!");
        Console.WriteLine($"{player.Name} отдыхает между врагами (+20 HP)");
        player.Heal(20);
        round = 1;
        enemyId++;
        continue; // Переходим к следующему врагу
    }

    // Ход оппонента
    dice = Random.Shared.Next(0, 20) + Random.Shared.Next(0, 20);

    if (dice > player.CD)
    {
        dice = Random.Shared.Next(0, 100);
        if (dice > player.EvadeChance)
        {
            double damage = currentEnemy.DoSomeDmg(1.2);
            player.TakeDamage(damage);
            Console.WriteLine($"{player.Name} получил урон от {currentEnemy.Name} ({dice}) - урон: {damage:F2}");
        }
        else
        {
            double damage = currentEnemy.DoSomeDmg(0.2);
            player.TakeDamage(damage);
            Console.WriteLine($"{player.Name} увернулся от атаки {currentEnemy.Name} ({dice}) - легкий урон: {damage:F2}");
        }
    }
    else
    {
        Console.WriteLine($"{currentEnemy.Name} неудачно атакует {player.Name} ({dice})");
    }

    round++;
    Thread.Sleep(500);
}
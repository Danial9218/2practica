// Интерфейс для нанесения урона
interface IDamageDealer
{
    double DoSomeDmg(double modifier);
}

// Интерфейс для получения опыта и уровней
interface ILevelable
{
    int Level { get; }
    double Experience { get; }
    void GainExperience(double exp);
    void LevelUp();
}
public interface IEnemy
{
    void TakeDamage(int damage);
    bool IsDead { get; }
}
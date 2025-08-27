using System;

public class PlayerHit
{
    public event Action OnHit;
    private int _damage;

    public PlayerHit(int damage)
    {
        _damage = damage;
    }

    public void Hit()
    {
        OnHit?.Invoke();
    }

    public void HitEnemy(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
    }
}

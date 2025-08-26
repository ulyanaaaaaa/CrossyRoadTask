using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, IProduct
{
    private EnemySettings _settings;
    private int _currentHealth;

    public bool IsDead => _currentHealth <= 0;

    public void Setup(EnemySettings settings)
    {
        _settings = settings;
        _currentHealth = _settings.MaxHealth;
        // Можно сразу настроить скорость движения и другие параметры
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
        // Добавить анимацию/эффекты
    }

    // Пример атаки игрока
    public void AttackPlayer(Player player)
    {
        if(player != null)
            player.TakeDamage(_settings.Damage);
    }

    public IProduct Initialize(float speed = 0, int damage = 0)
    {
        // В этом случае инициализация через EnemySettings
        return this;
    }
}
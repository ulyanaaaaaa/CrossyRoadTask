using System;

public class PlayerHealth
{
    public event Action OnDeath;

    private int _maxHealth;
    private int _currentHealth;

    public PlayerHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
    
    public int MaxHealth => _maxHealth;
}
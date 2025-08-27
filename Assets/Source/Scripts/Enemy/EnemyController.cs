using System;
using System.Collections.Generic;

public class EnemyController
{
    public event Action OnAllEnemiesDefeated;

    private readonly List<Enemy> _enemies = new();

    public void RegisterEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.OnDeath += OnEnemyDeath; 
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
        {
            OnAllEnemiesDefeated?.Invoke();
        }
    }
}

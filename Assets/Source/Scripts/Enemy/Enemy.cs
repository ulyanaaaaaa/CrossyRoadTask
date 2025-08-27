using System.Collections;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class Enemy : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;
    private int _damage;
    private float _attackInterval;
    private PlayerHealth _playerHealth;
    private bool _isAttacking;
    private Coroutine _attackCoroutine;

    private Vector3 _startPosition;
    private Tween _idleTween;

    public Enemy InitializeEnemy(PlayerHealth playerHealth, EnemySettings settings)
    {
        _playerHealth = playerHealth;
        _playerHealth.OnDeath += Die;

        _maxHealth = settings.MaxHealth;
        _currentHealth = _maxHealth;
        _damage = settings.Damage;
        _attackInterval = settings.AttackInterval;

        return this;
    }

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        _startPosition = transform.position;

        _attackCoroutine = StartCoroutine(AttackRoutine());

        StartIdleAnimation();
    }

    private void StartIdleAnimation()
    {
        _idleTween = transform
            .DOMoveX(_startPosition.x + Random.Range(-0.8f, 0.8f), 1.3f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        transform
            .DOMoveZ(_startPosition.z + Random.Range(-0.8f, 0.8f), 1.2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackInterval);
            Attack();
        }
    }

    private void Attack()
    {
        if (_isAttacking) return;
        _isAttacking = true;

        transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360)
            .SetRelative()
            .OnComplete(() => _isAttacking = false);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        _idleTween?.Kill(); 
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isAttacking && other.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_attackCoroutine);
        _playerHealth.OnDeath -= Die;
        _idleTween?.Kill();
    }
}

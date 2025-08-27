using System.Collections;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class Player : MonoBehaviour, IPerson
{
    [SerializeField] private float _secondsDelay;
    private CameraFollow _cameraFollow;
    private PlayerHealth _playerHealth;
    private PlayerHit _playerHit;
    private IInput _input;
    private SceneLoader _sceneLoader;
    private bool _isRun;
    private Vector3 _startPosition;
    private Vector3 _cameraStartPosition;
    private int _rotationDirection;
    private bool _isAttacking;
    
    [Inject]
    public void Init(IInput input, PlayerHealth playerHealth, PlayerHit playerHit, SceneLoader sceneLoader)
    {
        _input = input;
        _playerHit = playerHit;
        _playerHealth = playerHealth;
        _sceneLoader =  sceneLoader;
    }

    private void OnEnable()
    {
        _playerHealth.OnDeath += HandleDeath;
        _playerHit.OnHit += Hit;
        _input.OnBack += OnBack;
        _input.OnLeft += OnLeft;
        _input.OnRight += OnRight;
        _input.OnRun += OnRun;
        _sceneLoader.OnReload += HandleDeath;
    }

    private void Start()
    {
        _rotationDirection = 1;
        
        _cameraFollow = Camera.main?.GetComponent<CameraFollow>();
        _startPosition = transform.position;
        if (_cameraFollow != null)
            _cameraStartPosition = _cameraFollow.transform.position;
    }

    public void TakeDamage(int damage)
    {
        _playerHealth.TakeDamage(damage);
    }

    private void HandleDeath()
    {
        transform.DOKill();

        transform.position = _startPosition;
        _isRun = false;

        _playerHealth.Heal(_playerHealth.MaxHealth);

        if (_cameraFollow != null)
            _cameraFollow.MoveTo(_cameraStartPosition);
    }


    private void Update()
    {
        _input.Detect();
    }

    private void OnRun()
    {
        if (_isRun)  
            return; 
        StartCoroutine(Move(new Vector3(-1, 0, 0)));
    }

    private void OnLeft()
    {
        if (_isRun) 
            return;
        StartCoroutine(Move(new Vector3(0, 0, -1)));
    }

    private void OnRight()
    {
        if (_isRun) 
            return; 
        StartCoroutine(Move(new Vector3(0, 0, 1)));
    }

    private void OnBack()
    {
        if (_isRun) 
            return; 
        
        StartCoroutine(Move(new Vector3(1, 0, 0)));
    }

    private void Hit()
    {
        _isAttacking = true;

        transform
            .DORotate(new Vector3(0, _rotationDirection * 360, 0), 0.7f, RotateMode.FastBeyond360)
            .SetRelative()
            .SetEase(Ease.OutBack)
            .OnComplete(() => _isAttacking = false);

        _rotationDirection *= -1;
    }


    private IEnumerator Move(Vector3 difference)
    {
        if (difference == Vector3.zero) yield break;

        _isRun = true;

        _cameraFollow.Follow(difference);
        transform.rotation = Quaternion.LookRotation(difference, Vector3.up);
        transform.DOJump(transform.position + difference, 1f, 1, 0.2f);

        yield return new WaitForSeconds(_secondsDelay);
        _isRun = false;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (_isAttacking && other.TryGetComponent(out Enemy enemy))
        {
            _playerHit.HitEnemy(enemy);
        }
    }
    
    private void OnDisable()
    {
        _input.OnBack -= OnBack;
        _input.OnLeft -= OnLeft;
        _input.OnRight -= OnRight;
        _input.OnRun -= OnRun;
        _playerHealth.OnDeath -= HandleDeath;
        _playerHit.OnHit -= Hit;
        _sceneLoader.OnReload -= HandleDeath;

    }
}

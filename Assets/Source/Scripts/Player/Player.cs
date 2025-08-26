using System.Collections;
using UnityEngine;
using DG.Tweening;
using Zenject;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _secondsDelay;
    private CameraFollow _cameraFollow;
    private PlayerHealth _playerHealth;
    private IInput _input;
    private bool _isRun;
    private Vector3 _startPosition;
    private Vector3 _cameraStartPosition;


    [Inject]
    public void Init(IInput input, PlayerHealth playerHealth)
    {
        _input = input;
        _playerHealth = playerHealth;
        _playerHealth.OnDeath += HandleDeath;
    }

    private void OnEnable()
    {
        _input.OnBack += OnBack;
        _input.OnLeft += OnLeft;
        _input.OnRight += OnRight;
        _input.OnRun += OnRun;
    }

    private void Start()
    {
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

    private IEnumerator Move(Vector3 difference)
    {
        if (difference == Vector3.zero) yield break;

        _isRun = true;

        _cameraFollow.Follow(difference);
        transform.rotation = Quaternion.LookRotation(difference, Vector3.up);
        GetComponent<Animator>().SetTrigger("IsJump");
        transform.DOJump(transform.position + difference, 1f, 1, 0.2f);

        yield return new WaitForSeconds(_secondsDelay);
        _isRun = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StartBattleZone>())
        {
            
        }
    }

    private void OnDisable()
    {
        _input.OnBack -= OnBack;
        _input.OnLeft -= OnLeft;
        _input.OnRight -= OnRight;
        _input.OnRun -= OnRun;

        if (_playerHealth != null)
            _playerHealth.OnDeath -= HandleDeath;
    }
}

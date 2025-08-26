using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _secondsDelay;
    private CameraFollow _cameraFollow;
    private IInput _input;
    private bool _isRun;

    [Inject]
    public void Init(IInput input)
    {
        _input =  input;
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
    }

    private void Update()
    {
        _input.Detect();
    }

    private void OnRun()
    {
        if (_isRun)
            return;
        
        StartCoroutine(Move(new Vector3(-1,0,0)));
    }

    private void OnLeft()
    {
        if (_isRun)
            return;
        
        StartCoroutine(Move(new Vector3(0,0,-1)));
    }

    private void OnRight()
    {
        if (_isRun)
            return;
        
        StartCoroutine(Move(new Vector3(0,0,1)));
    }

    private void OnBack()
    {
        if (_isRun)
            return;
        
        StartCoroutine(Move(new Vector3(1,0,0)));
    }
    
    private IEnumerator Move(Vector3 difference)
    {
        if (difference == Vector3.zero)
            yield return null;
        
        _isRun = true;
        
        _cameraFollow.Follow(difference);
        
        Quaternion targetRotation = Quaternion.LookRotation(difference, Vector3.up);
        transform.rotation = targetRotation;
        
        transform.DOJump(transform.position + difference, 1f, 1, 0.2f);
        yield return new WaitForSeconds(_secondsDelay);
        _isRun = false;
    }
    
    private void OnDisable()
    {
        _input.OnBack -= OnBack;
        _input.OnLeft -= OnLeft;
        _input.OnRight -= OnRight;
        _input.OnRun -= OnRun;
    }
}
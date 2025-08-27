using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

[RequireComponent(typeof(Button))]
public class HitButton : MonoBehaviour
{
    private Button _button;
    private PlayerHit _playerHit;
    private Vector3 _originalScale;
    private Tween _pressTween;

    [SerializeField] private float _pressAmount; 
    [SerializeField] private float _pressDuration; 

    [Inject]
    public void Init(PlayerHit playerHit)
    {
        _playerHit = playerHit;
    }
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Hit);
        StartPressEffect();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Hit);
        _pressTween?.Kill();
        transform.localScale = _originalScale;
    }

    private void Hit()
    {
        _playerHit.Hit();
    }

    private void StartPressEffect()
    {
        _pressTween = transform
            .DOScale(_originalScale * _pressAmount, _pressDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.OutBack);
    }
}
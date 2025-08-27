using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OpenButton : MonoBehaviour
{
    private Button _button;
    private Vector3 _originalScale;
    private Tween _pressTween;
    private BoxController _boxController;

    [SerializeField] private float _pressAmount; 
    [SerializeField] private float _pressDuration;

    [Inject]
    public void Init(BoxController boxController)
    {
        _boxController = boxController;
    }

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _originalScale = transform.localScale;
        _button.onClick.AddListener(Open);
        StartPressEffect();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Open);
        _pressTween?.Kill();
        transform.localScale = _originalScale;
    }

    public void Open()
    {
        _boxController.OpenBox();
    }

    private void StartPressEffect()
    {
        _pressTween = transform
            .DOScale(_originalScale * _pressAmount, _pressDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.OutBack);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

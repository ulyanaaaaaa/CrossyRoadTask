using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _moveDuration; 
    private Tween _currentTween;

    public void Follow(Vector3 difference)
    {
        _currentTween?.Kill();

        Vector3 targetPosition = transform.position + difference;
        _currentTween = transform.DOMove(targetPosition, _moveDuration)
            .SetEase(Ease.OutSine);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _currentTween?.Kill();
        _currentTween = transform.DOMove(targetPosition, _moveDuration)
            .SetEase(Ease.OutSine);
    }
}
using UnityEngine;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private float _scaleUp; 
    [SerializeField] private float _duration; 

    private void OnEnable()
    {
        Vector3 baseScale = transform.localScale;

        transform.DOScale(baseScale * _scaleUp, _duration)
            .SetLoops(-1, LoopType.Yoyo)   
            .SetEase(Ease.InOutSine);      
    }
}
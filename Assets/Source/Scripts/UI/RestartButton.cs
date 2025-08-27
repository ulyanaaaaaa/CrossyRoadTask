using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    [SerializeField] private float _rotationDuration; 
    [SerializeField] private float _rotationAngle;
    private Button _button;
    private SceneLoader _sceneLoader;
    
    [Inject]
    public void Init(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    private void OnEnable()
    {
        transform.DORotate(new Vector3(0f, 0f, _rotationAngle), _rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
        
        _button.onClick.AddListener(_sceneLoader.ReloadScene);
    }
    
    private void OnDisable()
    {
        transform.DOKill();
        _button.onClick.RemoveListener(_sceneLoader.ReloadScene);

    }
}

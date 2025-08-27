using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class Key : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image _image;
    private CanvasGroup _canvasGroup;

    private Vector3 _startPosition;   
    private Transform _startParent;   

    public Color Color => _image.color;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public Key Setup(Color newColor)
    {
        _image.color = newColor;
        return this;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _startParent = transform.parent;

        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnToStart();
        _canvasGroup.blocksRaycasts = true;
    }

    public void ReturnToStart()
    {
        transform.SetParent(_startParent);
        transform.position = _startPosition;
    }

    public void Hide()
    {
        var color = _image.color;
        color.a = 0f;
        _image.color = color;
        _image.enabled = false;
    }
}
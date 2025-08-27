using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    private BoxView _boxView;

    private void Awake()
    {
        _boxView = GetComponentInParent<BoxView>(); 
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) 
            return;

        Key droppedKey = eventData.pointerDrag.GetComponent<Key>();
        if (droppedKey == null) return;

        if (_boxView.CheckKey(droppedKey.Color))
        {
            _boxView.AddKey();
            droppedKey.Hide();
        }
        else
        {
            droppedKey.ReturnToStart();
        }
    }
}
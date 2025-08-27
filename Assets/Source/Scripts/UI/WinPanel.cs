using UnityEngine;

public class WinPanel : MonoBehaviour
{
    public void Open()
    { 
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

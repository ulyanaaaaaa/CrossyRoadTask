using UnityEngine;
using Zenject;

public class Box : MonoBehaviour
{
    private OpenButton _openButton;
    
    public Box Setup(OpenButton openButton)
    {
        _openButton = openButton;
        return this;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _openButton.Show();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _openButton.Hide();
        }
    }
}
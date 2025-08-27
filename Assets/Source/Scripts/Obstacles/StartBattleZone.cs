using UnityEngine;
using System;

public class StartBattleZone : MonoBehaviour
{
    public event Action OnPlayerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (OnPlayerEntered != null)
                Debug.Log("OnPlayerEntered has subscribers");
            
            Debug.Log("OnTriggerEnter");
            OnPlayerEntered?.Invoke();
        }
    }
}
using UnityEngine;
using System;

public class StartBattleZone : MonoBehaviour
{
    public event Action OnPlayerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnPlayerEntered?.Invoke();
        }
    }
}
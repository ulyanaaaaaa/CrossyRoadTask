using System;
using UnityEngine;

public class DesktopInput : IInput
{
    public event Action OnRight;
    public event Action OnLeft;
    public event Action OnBack;
    public event Action OnRun;

    public void Detect()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            OnRight?.Invoke();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            OnLeft?.Invoke();

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            OnBack?.Invoke();

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            OnRun?.Invoke();
    }
}
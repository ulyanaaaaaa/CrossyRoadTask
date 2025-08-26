using System;

public interface IInput
{
    public event Action OnRight;
    public event Action OnLeft;
    public event Action OnBack;
    public event Action OnRun;

    public void Detect();
}
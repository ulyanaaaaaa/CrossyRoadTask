using System;

public class SceneLoader 
{
    public event Action OnReload;

    public void ReloadScene()
    {
        OnReload?.Invoke();
    }
}

using UnityEngine;
using Zenject;

public class BoxController
{
    private BoxView _boxView;

    public BoxController(BoxView boxView)
    {
        _boxView = boxView;
    }
    
    public void OpenBox()
    {
        _boxView.Open();
    }

    
}


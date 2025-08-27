using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoxView : MonoBehaviour
{
    [SerializeField] private Image _lockImage;
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private GridLayoutGroup _keysParent;
    [SerializeField] private Key _keyPrefab;

    private WinPanel _winPanel;
    private BoxSettings _boxSettings;
    private int _currentKeys;

    private Color _targetColor;

    [Inject]
    public void Init(BoxSettings boxSettings,  WinPanel winPanel)
    {
        _boxSettings = boxSettings;
        _winPanel = winPanel;
    }

    private void OnEnable()
    {
        _targetColor = _boxSettings.PossibleColors[
            Random.Range(0, _boxSettings.PossibleColors.Length)
        ];
        _lockImage.color = _targetColor;

        _currentKeys = 0;
        UpdateCounter();

        foreach (Transform child in _keysParent.transform)
            Destroy(child.gameObject);

        int totalKeys = _boxSettings.GridSize * _boxSettings.GridSize;
        List<Color> keyColors = new List<Color>();

        for (int i = 0; i < _boxSettings.RequiredKeys; i++)
        {
            keyColors.Add(_targetColor);
        }

        for (int i = _boxSettings.RequiredKeys; i < totalKeys; i++)
        {
            keyColors.Add(_boxSettings.PossibleColors[
                Random.Range(0, _boxSettings.PossibleColors.Length)
            ]);
        }

        for (int i = 0; i < keyColors.Count; i++)
        {
            int rand = Random.Range(i, keyColors.Count);
            (keyColors[i], keyColors[rand]) = (keyColors[rand], keyColors[i]);
        }

        foreach (var color in keyColors)
        {
            Instantiate(_keyPrefab, _keysParent.transform).Setup(color);
        }
    }

    private void UpdateCounter()
    {
        _counterText.text = $"{_currentKeys}/{_boxSettings.RequiredKeys}";
    }

    public void AddKey()
    {
        if (_currentKeys < _boxSettings.RequiredKeys)
        {
            _currentKeys++;
            UpdateCounter();
        }

        if (_currentKeys == _boxSettings.RequiredKeys)
        {
            _winPanel.Open();
            Close();
        }
    }
    
    public bool CheckKey(Color color)
    {
        return color == _targetColor;
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

}

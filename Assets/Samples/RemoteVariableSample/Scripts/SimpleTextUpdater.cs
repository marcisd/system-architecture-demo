using MSD;
using TMPro;
using UnityEngine;

public class SimpleTextUpdater : MonoBehaviour
{
    [SerializeField] private StringReference _textSource;

    [SerializeField] private TMP_Text _textRenderer;
    
    private void OnEnable()
    {
        _textSource.OnValueChanged += UpdateText;
    }

    private void OnDisable()
    {
        _textSource.OnValueChanged -= UpdateText;
    }

    private void Start()
    {
        UpdateText(_textSource);
    }

    private void UpdateText(string text)
    {
        if (_textRenderer != null)
        {
            _textRenderer.text = text;
        }
    }
}

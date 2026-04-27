using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PasswordDigitButton : MonoBehaviour
{
    [SerializeField] private TMP_Text digitText;

    private Button button;
    private int value;

    public int Value => value;

    public event Action DigitChanged;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (digitText == null)
        {
            digitText = GetComponentInChildren<TMP_Text>();
        }

        button.onClick.AddListener(IncrementDigit);
        UpdateView();
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(IncrementDigit);
        }
    }

    public void ResetDigit()
    {
        value = 0;
        UpdateView();
        DigitChanged?.Invoke();
    }

    private void IncrementDigit()
    {
        value = (value + 1) % 10;

        UpdateView();
        DigitChanged?.Invoke();
    }

    private void UpdateView()
    {
        if (digitText != null)
        {
            digitText.text = value.ToString();
        }
    }
}
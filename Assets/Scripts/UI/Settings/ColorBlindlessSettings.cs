using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorBlindnessSettings : MonoBehaviour
{
    public TextMeshProUGUI currentModeText;
    [SerializeField] private int currentIndex;

    private string[] modes = { "Normal", "Protanopia", "Deuteranopia", "Tritanopia" };

    private void Start()
    {
        currentIndex = System.Array.IndexOf(modes, GameSettingsManager.Instance.Settings.ColorBlindMode);
        if (currentIndex == -1)
        {
            currentIndex = 0; // Default to "Normal" if the saved mode is invalid
        }
        UpdateText();
    }


    public void NextMode()
    {
        currentIndex = (currentIndex + 1) % modes.Length;
        ApplyCurrentMode();
    }

    public void PreviousMode()
    {
        currentIndex = (currentIndex - 1 + modes.Length) % modes.Length;
        ApplyCurrentMode();
    }

    private void ApplyCurrentMode()
    {
        GameSettingsManager.Instance.SetColorBlindMode(modes[currentIndex]);
        UpdateText();
    }

    private void UpdateText()
    {
        currentModeText.text = $"Mode: {modes[currentIndex]}";
    }
}

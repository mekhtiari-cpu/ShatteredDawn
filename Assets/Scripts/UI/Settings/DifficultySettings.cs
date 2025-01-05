using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultySettings : MonoBehaviour
{
    public TextMeshProUGUI currentDifficultyText;
    [SerializeField] private int currentIndex;

    private string[] difficulties = { "Easy", "Normal", "Hard" };

    private void Start()
    {
        currentIndex = System.Array.IndexOf(difficulties, GameSettingsManager.Instance.Settings.Difficulty);
        if (currentIndex == -1)
        {
            currentIndex = 1; // Default to "Medium" if the saved difficulty is invalid
        }
        UpdateText();
    }

    public void NextDifficulty()
    {
        currentIndex = (currentIndex + 1) % difficulties.Length;
        ApplyCurrentDifficulty();
    }

    public void PreviousDifficulty()
    {
        currentIndex = (currentIndex - 1 + difficulties.Length) % difficulties.Length;
        ApplyCurrentDifficulty();
    }

    private void ApplyCurrentDifficulty()
    {
        GameSettingsManager.Instance.SetDifficulty(difficulties[currentIndex]);
        UpdateText();
    }

    private void UpdateText()
    {
        currentDifficultyText.text = $"{difficulties[currentIndex]}";
    }
}

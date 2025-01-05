using UnityEngine;

[System.Serializable]
public class GameSettings
{
    // Accessibility
    public string ColorBlindMode { get; set; }
    public string TextSize { get; set; }
    public float Brightness { get; set; }

    // Sound
    public float MasterVolume { get; set; }
    public float MusicVolume { get; set; }
    public float EffectsVolume { get; set; }
    public float UISoundVolume { get; set; }
    public float HostileVolume { get; set; }
    public float EnvironmentVolume { get; set; }

    // Display
    public int QualityLevel { get; set; }

    // Performance
    public int TargetFPS { get; set; }
    public bool VSync { get; set; }
    public bool DisplayFPS { get; set; }

    // Gameplay
    public float MouseSensitivity { get; set; }
    public string Difficulty { get; set; }

    public GameSettings()
    {
        ColorBlindMode = "Normal";
        TextSize = "Medium";
        MasterVolume = 1.0f;
        MusicVolume = 1.0f;
        EffectsVolume = 1.0f;
        UISoundVolume = 1.0f;
        HostileVolume = 1.0f;
        EnvironmentVolume = 1.0f;
        QualityLevel = 2;
        Brightness = 1.0f;
        TargetFPS = 60;
        VSync = true;
        DisplayFPS = false;
        MouseSensitivity = 1.0f;
        Difficulty = "Normal";
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetString("ColorBlindMode", ColorBlindMode);
        PlayerPrefs.SetString("TextSize", TextSize);
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("EffectsVolume", EffectsVolume);
        PlayerPrefs.SetFloat("UISoundVolume", UISoundVolume);
        PlayerPrefs.SetFloat("HostileVolume", HostileVolume);
        PlayerPrefs.SetFloat("EnvironmentVolume", EnvironmentVolume);
        PlayerPrefs.SetInt("QualityLevel", QualityLevel);
        PlayerPrefs.SetFloat("Brightness", Brightness);
        PlayerPrefs.SetInt("TargetFPS", TargetFPS);
        PlayerPrefs.SetInt("VSync", VSync ? 1 : 0);
        PlayerPrefs.SetInt("DisplayFPS", DisplayFPS ? 1 : 0);
        PlayerPrefs.SetFloat("MouseSensitivity", MouseSensitivity);
        PlayerPrefs.SetString("Difficulty", Difficulty);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        ColorBlindMode = PlayerPrefs.GetString("ColorBlindMode", "Normal");
        TextSize = PlayerPrefs.GetString("TextSize", "Medium");
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        EffectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1.0f);
        UISoundVolume = PlayerPrefs.GetFloat("UISoundVolume", 1.0f);
        HostileVolume = PlayerPrefs.GetFloat("HostileVolume", 1.0f);
        EnvironmentVolume = PlayerPrefs.GetFloat("EnvironmentVolume", 1.0f);
        QualityLevel = PlayerPrefs.GetInt("QualityLevel", 2);
        Brightness = PlayerPrefs.GetFloat("Brightness", 1.0f);
        TargetFPS = PlayerPrefs.GetInt("TargetFPS", 60);
        VSync = PlayerPrefs.GetInt("VSync", 1) == 1;
        DisplayFPS = PlayerPrefs.GetInt("DisplayFPS", 0) == 1;
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        Difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
    }
}

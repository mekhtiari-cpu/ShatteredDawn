using UnityEngine;

[System.Serializable]
public class GameSettings
{
    public string ColorBlindMode { get; set; }
    public float MasterVolume { get; set; }
    public float MusicVolume { get; set; }
    public float EffectsVolume { get; set; }
    public int ResolutionIndex { get; set; }
    public int QualityLevel { get; set; }
    public bool Fullscreen { get; set; }
    public int TargetFPS { get; set; }
    public bool VSync { get; set; }
    public bool DisplayFPS { get; set; }

    public GameSettings()
    {
        // Default values
        ColorBlindMode = "Normal";
        MasterVolume = 1.0f;
        MusicVolume = 1.0f;
        EffectsVolume = 1.0f;
        ResolutionIndex = 0;
        QualityLevel = 2;
        Fullscreen = true;
        TargetFPS = 60;
        VSync = true;
        DisplayFPS = false;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetString("ColorBlindMode", ColorBlindMode);
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("EffectsVolume", EffectsVolume);
        PlayerPrefs.SetInt("ResolutionIndex", ResolutionIndex);
        PlayerPrefs.SetInt("QualityLevel", QualityLevel);
        PlayerPrefs.SetInt("Fullscreen", Fullscreen ? 1 : 0);
        PlayerPrefs.SetInt("TargetFPS", TargetFPS);
        PlayerPrefs.SetInt("VSync", VSync ? 1 : 0);
        PlayerPrefs.SetInt("DisplayFPS", DisplayFPS ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        ColorBlindMode = PlayerPrefs.GetString("ColorBlindMode", "Normal");
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        EffectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1.0f);
        ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        QualityLevel = PlayerPrefs.GetInt("QualityLevel", 2);
        Fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        TargetFPS = PlayerPrefs.GetInt("TargetFPS", 60);
        VSync = PlayerPrefs.GetInt("VSync", 1) == 1;
        DisplayFPS = PlayerPrefs.GetInt("DisplayFPS", 0) == 1;
    }
}

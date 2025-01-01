using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance { get; private set; }
    public GameSettings Settings;
    private Resolution[] availableResolutions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Settings = new GameSettings();
            Settings.LoadSettings();
            availableResolutions = Screen.resolutions;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplySettings()
    {
        // Apply color blindness mode
        FindObjectOfType<ColorBlindnessManager>()?.SetColorBlindMode(Settings.ColorBlindMode);

        // Apply volume
        AudioListener.volume = Settings.MasterVolume;

        // Apply resolution
        if (Settings.ResolutionIndex >= 0 && Settings.ResolutionIndex < availableResolutions.Length)
        {
            Resolution res = availableResolutions[Settings.ResolutionIndex];
            Screen.SetResolution(res.width, res.height, Settings.Fullscreen);
        }

        // Apply quality level
        QualitySettings.SetQualityLevel(Settings.QualityLevel);

        // Apply FPS and VSync
        Application.targetFrameRate = Settings.TargetFPS;
        QualitySettings.vSyncCount = Settings.VSync ? 1 : 0;
    }

    public void SetColorBlindMode(string mode)
    {
        Settings.ColorBlindMode = mode;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetMasterVolume(float volume)
    {
        Settings.MasterVolume = volume;
        ApplySettings() ;
        Settings.SaveSettings();
    }

    public void SetTargetFPS(int fps)
    {
        Settings.TargetFPS = fps;
        Application.targetFrameRate = fps; // Apply the frame rate

        if (fps > 0) // Disable VSync for specific FPS limits
        {
            SetVSync(false);
        }

        Settings.SaveSettings();
    }


    public void SetResolution(int index)
    {
        Settings.ResolutionIndex = index;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetQualityLevel(int level)
    {
        Settings.QualityLevel = level;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Settings.Fullscreen = isFullscreen;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetVSync(bool enabled)
    {
        Settings.VSync = enabled;
        ApplySettings();
        Settings.SaveSettings();
    }
}

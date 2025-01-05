using System;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance { get; private set; }
    public GameSettings Settings;
    private Resolution[] availableResolutions;

    public int currentIndex;
    public Transform[] settingTabs;

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

    public void OpenSettings()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        OpenTab(0);
    }

    public void CloseSettings()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OpenTab(int index)
    {
        settingTabs[currentIndex].gameObject.SetActive(false);
        currentIndex = index;
        settingTabs[currentIndex].gameObject.SetActive(true);
    }

    public void ApplySettings()
    {
        // Apply color blindness mode
        FindObjectOfType<PostProcessorManager>()?.SetColorBlindMode(Settings.ColorBlindMode);

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

        // Apply brightness
        RenderSettings.ambientLight = Color.white * Settings.Brightness;

        // Apply mouse sensitivity
        // This would need to be applied in your input handler.

        // Apply text size
        // Adjust UI text sizes based on Settings.TextSize.

        // Apply difficulty
        // Ensure difficulty level is reflected in gameplay systems.
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
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetMusicVolume(float volume)
    {
        Settings.MusicVolume = volume;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetEffectsVolume(float volume)
    {
        Settings.EffectsVolume = volume;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetUISoundVolume(float volume)
    {
        Settings.UISoundVolume = volume;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetHostileVolume(float volume)
    {
        Settings.HostileVolume = volume;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetEnvironmentVolume(float volume)
    {
        Settings.EnvironmentVolume = volume;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetTargetFPS(int fps)
    {
        Settings.TargetFPS = fps;
        Application.targetFrameRate = fps;
        if (fps > 0)
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

    public void SetBrightness(float brightness)
    {
        Settings.Brightness = brightness;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        Settings.MouseSensitivity = sensitivity;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetTextSize(string size)
    {
        Settings.TextSize = size;
        ApplySettings();
        Settings.SaveSettings();
    }

    public void SetDifficulty(string difficulty)
    {
        Settings.Difficulty = difficulty;
        ApplySettings();
        Settings.SaveSettings();
    }
}

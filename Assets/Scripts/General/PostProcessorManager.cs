using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessorManager : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;

    #region ColorGrading - Color Blindness
    private ColorGrading defaultColorGrading;
    private ColorGrading protanopiaColorGrading;
    private ColorGrading deuteranopiaColorGrading;
    private ColorGrading tritanopiaColorGrading;

    private ColorGrading activeColorGrading;
    #endregion


    private void Start()
    {
        if (postProcessVolume == null)
        {
            return;
        }

        // Initialize color grading profiles
        postProcessVolume.profile.TryGetSettings(out activeColorGrading);

        // Set up default color grading
        defaultColorGrading = CreateColorGradingSettings();

        // Set up Protanopia color grading
        protanopiaColorGrading = CreateColorGradingSettings(
            mixerRed: new Vector3(56.667f, 43.333f, 0f),
            mixerGreen: new Vector3(55.833f, 44.167f, 0f),
            mixerBlue: new Vector3(0f, 24.167f, 75.833f));

        // Set up Deuteranopia color grading
        deuteranopiaColorGrading = CreateColorGradingSettings(
            mixerRed: new Vector3(62.5f, 37.5f, 0f),
            mixerGreen: new Vector3(70f, 30f, 0f),
            mixerBlue: new Vector3(0f, 30f, 70f));

        // Set up Tritanopia color grading
        tritanopiaColorGrading = CreateColorGradingSettings(
            mixerRed: new Vector3(95f, 5f, 0f),
            mixerGreen: new Vector3(0f, 43.333f, 56.667f),
            mixerBlue: new Vector3(0f, 47.5f, 52.5f));
    }

    public void SetColorBlindMode(string mode)
    {
        if (activeColorGrading == null)
        {
            return;
        }

        switch (mode)
        {
            case "Protanopia":
                ApplyColorGrading(protanopiaColorGrading);
                break;
            case "Deuteranopia":
                ApplyColorGrading(deuteranopiaColorGrading);
                break;
            case "Tritanopia":
                ApplyColorGrading(tritanopiaColorGrading);
                break;
            default:
                ApplyColorGrading(defaultColorGrading);
                break;
        }
    }
    private ColorGrading CreateColorGradingSettings(Vector3? mixerRed = null, Vector3? mixerGreen = null, Vector3? mixerBlue = null)
    {
        var colorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        colorGrading.mixerRedOutRedIn.value = mixerRed?.x ?? 100f;
        colorGrading.mixerRedOutGreenIn.value = mixerRed?.y ?? 0f;
        colorGrading.mixerRedOutBlueIn.value = mixerRed?.z ?? 0f;

        colorGrading.mixerGreenOutRedIn.value = mixerGreen?.x ?? 0f;
        colorGrading.mixerGreenOutGreenIn.value = mixerGreen?.y ?? 100f;
        colorGrading.mixerGreenOutBlueIn.value = mixerGreen?.z ?? 0f;

        colorGrading.mixerBlueOutRedIn.value = mixerBlue?.x ?? 0f;
        colorGrading.mixerBlueOutGreenIn.value = mixerBlue?.y ?? 0f;
        colorGrading.mixerBlueOutBlueIn.value = mixerBlue?.z ?? 100f;

        return colorGrading;
    }

    private void ApplyColorGrading(ColorGrading settings)
    {
        activeColorGrading.mixerRedOutRedIn.value = settings.mixerRedOutRedIn.value;
        activeColorGrading.mixerRedOutGreenIn.value = settings.mixerRedOutGreenIn.value;
        activeColorGrading.mixerRedOutBlueIn.value = settings.mixerRedOutBlueIn.value;

        activeColorGrading.mixerGreenOutRedIn.value = settings.mixerGreenOutRedIn.value;
        activeColorGrading.mixerGreenOutGreenIn.value = settings.mixerGreenOutGreenIn.value;
        activeColorGrading.mixerGreenOutBlueIn.value = settings.mixerGreenOutBlueIn.value;

        activeColorGrading.mixerBlueOutRedIn.value = settings.mixerBlueOutRedIn.value;
        activeColorGrading.mixerBlueOutGreenIn.value = settings.mixerBlueOutGreenIn.value;
        activeColorGrading.mixerBlueOutBlueIn.value = settings.mixerBlueOutBlueIn.value;
    }
}

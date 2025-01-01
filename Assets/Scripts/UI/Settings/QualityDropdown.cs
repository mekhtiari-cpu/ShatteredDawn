using TMPro;
using UnityEngine;

public class QualityDropdown : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;

    private void Start()
    {
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));
        qualityDropdown.value = GameSettingsManager.Instance.Settings.QualityLevel;
        qualityDropdown.RefreshShownValue();
    }

    public void OnQualityLevelChanged(int index)
    {
        GameSettingsManager.Instance.SetQualityLevel(index);
    }
}

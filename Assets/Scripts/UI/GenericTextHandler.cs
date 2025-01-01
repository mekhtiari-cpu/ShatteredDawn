using TMPro;
using UnityEngine;

/* -------------------------------------------------
* Standard Update Text function
* Overrides for different inputs (int, float, string)
* In Unity set a prefix e.g. 'Day: ' so we can append any data after it 
------------------------------------------------- */
public class GenericTextHandler : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string prefix;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        prefix = text.text;
    }

    public void UpdateText(int value)
    {
        text.text = prefix + value.ToString();
    }
}

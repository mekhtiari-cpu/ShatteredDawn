using UnityEngine;

public class StandardNPC : MonoBehaviour, INPC
{
    private string dialogue = "String 1";

    public void Interact()
    {
        Debug.Log(dialogue);
    }

    public string GetNPCType()
    {
        return "StandardNPC";
    }
}

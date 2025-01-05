using UnityEngine;

public class GunLibrary : MonoBehaviour
{

    public Gun[] allGuns;
    public static Gun[] guns;

    private void Awake()
    {
        allGuns = Resources.LoadAll<Gun>("Scriptable Objects/Guns");
        guns = allGuns;
    }

    public static Gun FindGun(string name)
    {
        foreach (Gun a in guns)
        {
            if (a.name == name)
            {
                return a;
            }
        }
        return guns[0];
    }
}
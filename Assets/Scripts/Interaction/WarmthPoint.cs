using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmthPoint : Interactable
{
    [SerializeField] private float warmthIntensity;
    public Player_Temperature_Manager ptm;

    public float GetWarmthIntensity()
    {
        return warmthIntensity;
    }
}

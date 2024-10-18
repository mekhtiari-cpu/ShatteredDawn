using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmthPoint : Interactable
{
    [SerializeField] private float warmthIntensity;
    [SerializeField] Player_Temperature_Manager ptm;

    public override void Interact()
    {
        base.Interact();
        ptm.SetWarmthStatus(true, warmthIntensity);
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        ptm.SetWarmthStatus(false, warmthIntensity);
    }

    public float GetWarmthIntensity()
    {
        return warmthIntensity;
    }
}

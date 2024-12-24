using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : WarmthPoint
{
    public override void Interact()
    {
        base.Interact();
        ptm.SetWarmthStatus(true, true, GetWarmthIntensity());
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        if(PlayerEquipment.instance.GetUtility() == null)
        {
            ptm.SetWarmthStatus(false, false, 0);
        }
        else
        {
            ptm.SetWarmthStatus(true, false, 1.05f);
        }
    }
}

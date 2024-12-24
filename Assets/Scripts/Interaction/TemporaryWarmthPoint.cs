using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryWarmthPoint : WarmthPoint
{ 
    [Header("Other Variables")]
    [SerializeField] bool isFinite;
    [SerializeField] float maxDuration;
    [SerializeField] float useTime;
    [SerializeField] float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    public void Update()
    {
        if(isFinite)
        {
            useTime = Time.time - spawnTime;
            if(useTime >= maxDuration)
            {
                PlayerEquipment.instance.RemoveUtility();
                Destroy(this.gameObject);
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        ptm = FindFirstObjectByType<Player_Temperature_Manager>();
        if(ptm.GetIsNearWarmth())
        {
            ptm.SetWarmthStatus(true, false, ptm.GetTempScalar());
        }
        else
        {
            ptm.SetWarmthStatus(true, false, GetWarmthIntensity());
        }
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        if(ptm.GetIsNearCampfire())
        {
            ptm.SetWarmthStatus(true, true, ptm.GetTempScalar());
        }
        else
        {
            ptm.SetWarmthStatus(false, false, 0);
        }
    }
}

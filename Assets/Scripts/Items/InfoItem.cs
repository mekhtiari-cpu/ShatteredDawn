using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Info Item", menuName = "Item/Info Item")]
public class InfoItem : Item
{
    public string infoTitle;
    public string infoDate;
    public string info;
    public override void UseItem()
    {
        Debug.Log(info);
        UI_Manager.instance.ReadInfo();
    }
}

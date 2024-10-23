using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoUI : MonoBehaviour
{
    [SerializeField] TMP_Text infoTitle;
    [SerializeField] TMP_Text infoDate;
    [SerializeField] TMP_Text infoData;


    public void DisplayInfo(Item item)
    {
        InfoItem infoItem = (InfoItem)item;

        infoTitle.text = infoItem.infoTitle;
        infoDate.text = infoItem.infoDate;
        infoData.text = infoItem.info; 
    }

    public void CloseInfo()
    {
        gameObject.SetActive(false);
    }
}

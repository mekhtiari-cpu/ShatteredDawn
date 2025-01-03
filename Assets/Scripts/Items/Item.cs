using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public abstract class Item : ScriptableObject
{
    public int itemId;
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] public bool isStackable;
    [SerializeField] public int count = 0;

    public abstract void UseItem();
}

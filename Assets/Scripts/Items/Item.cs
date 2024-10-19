using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] public bool isStackable;
    [SerializeField] public int count = 0;
    public enum ItemType {Craftable, Info, Equipable}
    public ItemType itemType;
}

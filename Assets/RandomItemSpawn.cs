using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawn : MonoBehaviour
{
    [Header("Item pickup objects")]
    [SerializeField] GameObject[] consumablePickups;
    [SerializeField] GameObject[] clothingPickups;
    [SerializeField] GameObject[] gunPickups;

    void Start()
    {
        RandomiseItems();
    }

    void RandomiseItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            print(transform.GetChild(i));
        }
    }

}

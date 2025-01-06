using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChunk : MonoBehaviour
{
    [SerializeField] GameObject[] itemsInThisArea;

    private void Start()
    {
        foreach (GameObject item in itemsInThisArea)
        {
            item.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(GameObject item in itemsInThisArea)
            {
                item.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach (GameObject item in itemsInThisArea)
            {
                item.SetActive(false);
            }
        }
    }
}

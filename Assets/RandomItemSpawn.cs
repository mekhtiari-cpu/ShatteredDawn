using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawn : MonoBehaviour
{
    [Header("Item pickup objects")]
    [SerializeField] GameObject[] consumablePickups;
    [SerializeField] GameObject[] utilityPickups;
    [SerializeField] GameObject[] clothingPickups;
    [SerializeField] GameObject[] gunPickups;

    [Header("Item quantities")]
    [SerializeField] int numConsumablesToBeSpawned;
    [SerializeField] int numUtilityToBeSpawned;
    [SerializeField] int numGunsToBeSpawned;
    [SerializeField] int numClothingToBeSpawned;

    [Header("Spawn Settings")]
    [SerializeField] bool[] openSlots;

    void Start()
    {
        RandomiseItems();
    }

    //Call all the randomise methods
    void RandomiseItems()
    {
        SpawnConsumables();
    }

    //Spawn the different items into random item slots around the map
    void SpawnConsumables()
    {
        for (int i = 0; i < numConsumablesToBeSpawned; i++)
        {
            int randSlot = Random.Range(0, transform.childCount-1);
            int consumableToBeSpawned = Random.Range(0, 101);

            while(openSlots[randSlot] == true)
            {
                randSlot = Random.Range(0, transform.childCount-1);
            }

            openSlots[randSlot] = true;

            if (consumableToBeSpawned <= 10)
                Instantiate(consumablePickups[2],transform.GetChild(randSlot));
            else if(consumableToBeSpawned <= 20)
                Instantiate(consumablePickups[1], transform.GetChild(randSlot));
            else
                Instantiate(consumablePickups[0], transform.GetChild(randSlot));
        }
        SpawnUtilities();
    }

    void SpawnUtilities()
    {
        for (int i = 0; i < numUtilityToBeSpawned; i++)
        {
            int randSlot = Random.Range(0, transform.childCount-1);

            while (openSlots[randSlot] == true)
            {
                randSlot = Random.Range(0, transform.childCount-1);
            }

            openSlots[randSlot] = true;
            Instantiate(utilityPickups[0], transform.GetChild(randSlot));
        }
        SpawnGuns();
    }

    void SpawnGuns()
    {
        for (int i = 0; i < numGunsToBeSpawned; i++)
        {
            int randSlot = Random.Range(0, transform.childCount-1);
            int gunToBeSpawned = Random.Range(0, 101);

            while (openSlots[randSlot] == true)
            {
                randSlot = Random.Range(0, transform.childCount-1);
            }

            openSlots[randSlot] = true;

            if (gunToBeSpawned <= 10)
                Instantiate(gunPickups[3], transform.GetChild(randSlot));
            else if (gunToBeSpawned <= 20)
                Instantiate(consumablePickups[2], transform.GetChild(randSlot));
            else if (gunToBeSpawned <= 30)
                Instantiate(consumablePickups[1], transform.GetChild(randSlot));
            else
                Instantiate(consumablePickups[0], transform.GetChild(randSlot));
        }
        SpawnClothing();
    }

    void SpawnClothing()
    {
        for (int i = 0; i < numClothingToBeSpawned; i++)
        {
            int randSlot = Random.Range(0, transform.childCount-1);
            int gunToBeSpawned = Random.Range(0, 101);

            while (openSlots[randSlot] == true)
            {
                randSlot = Random.Range(0, transform.childCount-1);
            }

            openSlots[randSlot] = true;

            if (gunToBeSpawned <= 50)
                Instantiate(clothingPickups[1], transform.GetChild(randSlot));
            else
                Instantiate(clothingPickups[0], transform.GetChild(randSlot));
        }
    }
}

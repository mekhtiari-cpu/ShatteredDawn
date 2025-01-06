using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderDistanceChecker : MonoBehaviour
{
    [SerializeField] float renderDistance;
    [SerializeField] GameObject zombie;

    private void Update()
    {
        if(Vector3.Distance(transform.position, GameManager.instance.player.position) > renderDistance)
        {
            zombie.SetActive(false);
        }
        else
        {
            zombie.SetActive(true);
        }
    }
}

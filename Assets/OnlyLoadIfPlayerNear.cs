using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyLoadIfPlayerNear : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject obj;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>().transform;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > 35f)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }
}

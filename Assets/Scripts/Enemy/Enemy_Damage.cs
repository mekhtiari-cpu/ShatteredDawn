using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] int minDamage;
    [SerializeField] int maxDamage;
    [SerializeField] int damage;

    private void Start()
    {
        damage = Random.Range(minDamage, maxDamage);
    }

    public int GetDamage()
    {
        return damage;
    }
}

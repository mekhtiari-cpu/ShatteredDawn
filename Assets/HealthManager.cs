using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float MaxHealth = 100;
    public float CurrentHealth;
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage) {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) {
            GameObject.SendMessage("Die");
        }
    }
}

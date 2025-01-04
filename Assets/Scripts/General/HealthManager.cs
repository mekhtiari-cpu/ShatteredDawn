using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float MaxHealth = 100;
    [SerializeField] private float CurrentHealth;
    public HealthBar health;
    void Start()
    {
        CurrentHealth = MaxHealth;
        if (health) 
        {
            health.setMaxHealth(CurrentHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage) {
        CurrentHealth -= damage;
        health.setHealth(CurrentHealth);

        if (CurrentHealth <= 0) {
            CurrentHealth = 0;
            gameObject.SendMessage("Die");
        }
        else
        {
            if(gameObject.tag == "Enemy")
            {
                gameObject.SendMessage("Hit");
            }
        }
        if(health)
        {
            health.setHealth(CurrentHealth);
        }
    }
}

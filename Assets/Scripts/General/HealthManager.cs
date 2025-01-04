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

    public bool GainHealth(int amount)
    {
        if(CurrentHealth < MaxHealth)
        {
            CurrentHealth += amount;
            if(CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            health.setHealth(CurrentHealth);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(float damage) {
        CurrentHealth -= damage;

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

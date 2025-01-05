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
        GameSettingsManager gsm = GameSettingsManager.Instance;
        float modifier = 1f;
        if(gsm)
        {
            switch (gsm.Settings.Difficulty)
            {
                case "Easy":
                    modifier = gameObject.tag == "Enemy" ? 0.8f : 1.25f;
                    break;

                case "Normal":
                    modifier = 1;
                    break;

                case "Hard":
                    modifier = gameObject.tag == "Enemy" ? 1.25f : .8f;
                    break;

                default:
                    modifier = 1;
                    break;

            }
        }

        CurrentHealth = MaxHealth * modifier;
        if (health) 
        {
            health.setMaxHealth(CurrentHealth);
        }
    }

    public bool GainHealth(int amount)
    {
        GameSettingsManager gsm = GameSettingsManager.Instance;
        float modifier = 1f;
        if (gsm)
        {
            switch (gsm.Settings.Difficulty)
            {
                case "Easy":
                    modifier = 1.25f;
                    break;

                case "Normal":
                    modifier = 1;
                    break;

                case "Hard":
                    modifier = .8f;
                    break;

                default:
                    modifier = 1;
                    break;

            }
        }

        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += amount * modifier;
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
        GameSettingsManager gsm = GameSettingsManager.Instance;
        float modifier = 1f;
        if (gsm)
        {
            switch (gsm.Settings.Difficulty)
            {
                case "Easy":
                    modifier = gameObject.tag != "Enemy" ? 0.8f : 1.25f;
                    break;

                case "Normal":
                    modifier = 1;
                    break;

                case "Hard":
                    modifier = gameObject.tag != "Enemy" ? 1.25f : .8f;
                    break;

                default:
                    modifier = 1;
                    break;

            }
        }
        CurrentHealth -= damage * modifier;

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

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }
}

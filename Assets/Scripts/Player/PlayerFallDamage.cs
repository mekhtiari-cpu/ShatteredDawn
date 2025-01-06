using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallDamage : MonoBehaviour
{
    [SerializeField] Player_Movement pm;
    [SerializeField] HealthManager myHealth;
    [SerializeField] Player_Death myDeath;
    [SerializeField] bool isGrounded;
    [SerializeField] bool hasAlreadyChecked;
    [SerializeField] Vector3 fallStartPosition;
    [SerializeField] Vector3 fallEndPosition;
    [SerializeField] float fallHeight;
    [SerializeField] float fallDamageMultiplier;
    [SerializeField] AudioSource fallDamageSound;

    private void Start()
    {
        pm = GetComponent<Player_Movement>();
        myHealth = GetComponent<HealthManager>();
        hasAlreadyChecked = false;
    }

    private void Update()
    {
        isGrounded = pm.GetIsGrounded();
        CheckFall();
    }

    //Check whether the player has fallen far enough to receive fall damage
    void CheckFall()
    {
        if(!isGrounded)
        {
            if(!hasAlreadyChecked)
            {
                hasAlreadyChecked = true;
                fallStartPosition = transform.position;
            }
        }
        else
        {
            if(hasAlreadyChecked)
            {
                fallEndPosition = transform.position;
                CalculateFallDamage();
                hasAlreadyChecked = false;
            }
        }
    }

    //Calculate the amount of damage to be dealt to player from fall
    void CalculateFallDamage()
    {
        Vector3 fallDistance = fallStartPosition - fallEndPosition;
        if(fallDistance.y > 7f)
        {
            myHealth.TakeDamage((int)fallDistance.y * fallDamageMultiplier);
            if(myHealth.GetCurrentHealth() <= 0f)
            {
                myDeath.SetCauseOfDeath("You fell to your death.");
            }
            fallDamageSound.Play();
        }
    }
}

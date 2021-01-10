using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (Random.Range(0, 2) == 0)
            SoundManagerScript.PlaySound("boar_hurt1");
        else
            SoundManagerScript.PlaySound("boar_hurt2");

        //play hurt animation

        if (currentHealth <= 0)
        {
            SoundManagerScript.PlaySound("boar_death");
            Destroy();
        }
    }

    void Destroy()
    {
        Debug.Log("Enemy is dead");

        //Die Animation
        //animator.SetBool("isDead", true);
        animator.Play("Boar Death");
        this.enabled = false;
    }
}

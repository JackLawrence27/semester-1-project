using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    public AudioSource audioSource;


    public float speed;
    public bool MoveRight;

    void Start()
    {
        currentHealth = maxHealth;

    }

    // Use this for initialization
    void Update()
    {
        // Use this for initialization
        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }
    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turn"))
        {

            if (MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (speed > 0f)
        {
            if (Random.Range(0, 2) == 0)
                SoundManagerScript.PlaySound("boar_hurt1");
            else
                SoundManagerScript.PlaySound("boar_hurt2");
        }

        //play hurt animation

        if (currentHealth <= 0)
        {
            if (speed > 0f)
            {
                SoundManagerScript.PlaySound("boar_death");
                Destroy();
            }
        }
    }

    void Destroy()
    {
        Debug.Log("Enemy is dead");
        audioSource.mute = !audioSource.mute;
        //Die Animation
        //animator.SetBool("isDead", true);
        animator.Play("Boar Death");
        speed = 0f;
        this.enabled = false;
    }
}

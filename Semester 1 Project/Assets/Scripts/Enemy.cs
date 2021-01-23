using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    public AudioSource audioSource;
    public string tagToIgnore = "enemy";
    public string tagToIgnoreWhenDead = "player";


    public float speed;
    public bool moveRight;

    void Start()
    {
        currentHealth = maxHealth;

    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tagToIgnore)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (currentHealth <= 0)
        {
            if (collision.gameObject.tag == tagToIgnoreWhenDead)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }


    // Use this for initialization
    void Update()
    {
        // Making sure the boar is facing and travelling in the right direction based on the moveRight variable
        if (moveRight)
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
        //If the boar hits the collider obiects I have set up through out the level it will flip and head in the reverse direction
        if (trig.gameObject.CompareTag("turn"))
        {

            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Making sure the boar's movement speed isn't less than zero meaning that it is still alive and to play a hurt sound when hit
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
        animator.Play("Boar Death");
        speed = 0f;
        this.enabled = false;
    }
}

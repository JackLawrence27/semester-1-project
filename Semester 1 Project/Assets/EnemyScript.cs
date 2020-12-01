using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    [SerializeField]
    float aggroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distToPlayer = Vector2.Distance(transform.position, Player.position);

        if (distToPlayer < aggroRange)
        {
            //Code to chase player
            Chase();
        }
        else
        {
            //Code to stop chasing player
            StopChase();
        }
    }
    void Chase()
    {
        if(transform.position.x < Player.position.x)
        {
            //Enemy is to the left of the player so move right >
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            //Enemy is to the right of the player so move left <
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

     void StopChase()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
}

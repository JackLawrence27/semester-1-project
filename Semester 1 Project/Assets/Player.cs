using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public Fields
    public float speed = 1;
 
    //Private Fields
    Rigidbody2D rb;
    Animator anim;
    float horizontalValue;
    float runSpeedModifier = 2;
    bool isRunning = false;
    bool facingRight = false;

    void Awake()
    {
        //Enable use of components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Storing the horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal");

        //lShift is enabling isRunning
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        //lShift is disabling isRunning
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;
    }

    void FixedUpdate()
    {
        Move(horizontalValue);
    }

    void Move(float dir)
    {
        //Set player speed based
        float xVal = dir * speed * 100 * Time.deltaTime;
        //If we are running multiply move speed
        if (isRunning)
            xVal *= runSpeedModifier;
        
        //Create Vector and assign player velocity
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        rb.velocity = targetVelocity;
        
        //If looking right and left swap object correctly to face right direction
        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if(!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        //Animation, 0/Idle, 4/Walking, 8/Running
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
}

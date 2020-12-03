using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

 
    //Private Fields
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] Collider2D standingCollider;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] float speed = 1;
    [SerializeField] float jumpSpeed = 500;
    const float overheadCheckRadius = 0.2f;
    const float groundCheckRadius = 0.2f;
    float horizontalValue;
    float runSpeedModifier = 2;

    //Not assigned as default is false;
    bool isGrounded;
    bool isRunning;
    bool crouchPressed;
    bool jump;
    
    
    bool facingRight;

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

        //Flip Check
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            if (Input.GetAxisRaw("Horizontal") < 0.5f && !facingRight)
            {
                //If we're moving right but not facing right, flip the sprite and set     facingRight to true.
                Flip();
                facingRight = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0.5f && facingRight)
            {
                //If we're moving left but not facing left, flip the sprite and set facingRight to false.
                Flip();
                facingRight = false;
            }
        }

        //If we press jump button enable/disable jump
        if (Input.GetButtonDown("Jump"))
            jump = true;
        else if (Input.GetButtonUp("Jump"))
            jump = false;

        //If we press jump button enable/disable crouch
        if (Input.GetButtonDown("Crouch"))
            crouchPressed = true;
        else if (Input.GetButtonUp("Crouch"))
            crouchPressed = false;
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, jump, crouchPressed);
    }

    void GroundCheck()
    {
        isGrounded = false;
        //Check if the player groundcheck is colliding with other colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;
    }

    void Move(float dir,bool jumpFlag,bool crouchFlag)
    {
        #region Jump And Crouch
        //When not crouching under a collider
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position,overheadCheckRadius,groundLayer))
                crouchFlag = true;
        }

        //If crouch pressed disable standing collider
        if (isGrounded)
        {
            standingCollider.enabled = !crouchFlag;


            //If played is grounded press jump
            if(jumpFlag)
            {
                isGrounded = false;
                jumpFlag = false;
                rb.AddForce(new Vector2(0f, jumpSpeed));
            }
        }

        anim.SetBool("Crouch", crouchFlag);
        #endregion



        #region Move And Run
        //Set player speed based
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        //If we are running multiply move speed
        if (isRunning)
            xVal *= runSpeedModifier;
        //If we are running multiply move speed
        if (crouchFlag)
            xVal *= runSpeedModifier/4;

        //Create Vector and assign player velocity
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        rb.velocity = targetVelocity;

        //Animation, 0/Idle, 4/Walking, 8/Running
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

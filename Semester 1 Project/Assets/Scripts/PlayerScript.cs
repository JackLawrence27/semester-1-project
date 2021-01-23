using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

 
    //Private Fields
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] Collider2D standingCollider;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] float speed = 0.5f;
    [SerializeField] float jumpSpeed = 500;
    [SerializeField] int totalJumps;
    int availableJumps;
    const float overheadCheckRadius = 0.2f;
    const float groundCheckRadius = 0.2f;
    float horizontalValue;
    float runSpeedModifier = 2;

    //Not assigned as default is false;
    public bool isGrounded;
    public bool crouchPressed;
    bool isRunning;
    bool facingRight;
    bool multipleJump;

    public string onDeath = "deathReset";

    //Coding
    void Awake()
    {
        availableJumps = totalJumps;
        //Enable use of components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == onDeath)
        {
            SceneManager.LoadScene("Game Scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Set Anim yVelocity
        anim.SetFloat("yVelocity", rb.velocity.y);

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
        {
            Jump();
        }

        //If we press jump button enable/disable crouch
        if (isGrounded)
        {
            if (Input.GetButtonDown("Crouch"))
                crouchPressed = true;
            else if (Input.GetButtonUp("Crouch"))
                crouchPressed = false;
        }

    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue,crouchPressed);
    }

    void GroundCheck()
    {
        //Track previous grounded state
        bool wasGrounded = isGrounded;
        isGrounded = false;

        //Check if the player groundcheck is colliding with other colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;

            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                jumpSpeed = 8f;
                multipleJump = false;
                Debug.Log("LANDED");
            }
        }


        //Disable Jump bool in animator
        anim.SetBool("Jump", !isGrounded);
    }

    void Jump()
    {
        //If crouch pressed disable standing collider
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;
        
            rb.velocity = Vector2.up * jumpSpeed;
            anim.SetBool("Jump", true);
            SoundManagerScript.PlaySound("jump_snd");
        } else
        {
            if(multipleJump && availableJumps > 0)
            {
                availableJumps--;
                jumpSpeed = jumpSpeed / 2 + 1.5f;
                rb.velocity = Vector2.up * jumpSpeed;
                anim.SetBool("Jump", true);
            }
        }
    }

    void Move(float dir,bool crouchFlag)
    {
        #region Crouch
        //When not crouching under a collider
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position,overheadCheckRadius,groundLayer))
                crouchFlag = true;
        }

        anim.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        #endregion



        #region Move And Run
        //Set player speed based
        float xVal = dir * speed * 75 * Time.fixedDeltaTime;
        //If we are running multiply move speed
        if (isRunning && !crouchFlag)
            xVal *= runSpeedModifier;
        //If we are crouching divide the speed by 4
        if (crouchFlag)
            xVal *= runSpeedModifier/4;

        //Debug.Log(xVal);
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


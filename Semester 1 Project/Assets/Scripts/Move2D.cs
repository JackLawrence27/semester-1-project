using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public bool isGrounded = false;
    public float animatorChecker = 0f;
    public bool facingRight = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        animatorChecker = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(animatorChecker));

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

        void Jump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
            }
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
}
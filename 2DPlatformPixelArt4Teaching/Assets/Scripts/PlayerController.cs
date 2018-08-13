using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 5f;

    public float JumpHeight = 5f;

    bool facingright = true;

    float move;

    bool isGrounded = true;

    Animator anim;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        // Move the character
        rb.velocity = new Vector2(move * Speed, rb.velocity.y);


        if (move > 0 && !facingright)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && facingright)
        {
            // ... flip the player.
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            isGrounded = false;

            rb.velocity = new Vector2(0, JumpHeight);

            anim.SetTrigger("Jump");

            anim.SetBool("Grounded", false);

            

        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingright = !facingright;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        anim.ResetTrigger("Jump");

        anim.SetBool("Grounded", true);

        isGrounded = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    public float speed = 5f;
    public float jumpForce = 15f;

    public bool isGrounded;
    private float moveInput;
    private Rigidbody2D rb;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Fixed update is called a certain amount of times per second and is unrelated to frames
    void FixedUpdate()
    {
        float moveInput = 0;
        
        //Move the player
        if (Input.GetKey(leftKey))
        {
            moveInput = -1;
        }
        else if (Input.GetKey(rightKey))
        {
            moveInput = 1;
        }
        //else
        //{
        //    rb.velocity = new Vector2(0 * speed, rb.velocity.y);
        //}

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);



        //Flip player depending which direction they are moving
        if (!facingRight && moveInput < 0)
        {
            Flip();
        }
        else if (facingRight && moveInput > 0)
        {
            Flip();
        }

        //Check if player can jump/wants to jump
        if (Input.GetKey(jumpKey) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
        {
            isGrounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag != "Player")
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
            isGrounded = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

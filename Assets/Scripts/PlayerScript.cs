using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode attackKey;

    public float speed = 5f;
    public float jumpForce = 15f;
    public bool isGrounded;
    private float moveInput = 0;

    private Rigidbody2D rb;
    private Animator myAnimator;

    private bool facingRight;

    public string playerName;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        this.name = playerName;
    }

    void SetName(string newName)
    {
        playerName = newName;
        this.name = playerName;
    }

    void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        myAnimator.SetFloat("Speed", Mathf.Abs(moveInput));

        // If the attack button is pressed, play the attack animation
        if (Input.GetKeyDown(attackKey) && moveInput == 0)
        {
            myAnimator.SetTrigger("Attack");
            PushOtherPlayer();
        }

        // Flip player depending which direction they are moving
        if (!facingRight && moveInput < 0)
        {
            Flip();
        }
        else if (facingRight && moveInput > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Move the player
        //if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        //{
            if (Input.GetKey(leftKey))
            {
                moveInput = -1;
            }
            else if (Input.GetKey(rightKey))
            {
                moveInput = 1;
            }
            else
            {
                moveInput = 0;
            }
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            //Check if player can jump/wants to jump
            if (Input.GetKey(jumpKey) && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        //}
    }

    void PushOtherPlayer()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, layerMask);

        Debug.DrawRay(transform.position, Vector2.right * 2.11f, Color.red, 20f);
    }

    void AddScore()
    {
        Debug.Log("I'm gaining points!");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Objective")
        {
            isGrounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag != "Objective")
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        isGrounded = false;
    }
}
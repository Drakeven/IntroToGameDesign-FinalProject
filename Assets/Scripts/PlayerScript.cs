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
    private float moveInput;
    private Rigidbody2D rb;
    private bool attack;
    private Animator myAnimator;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputAttack();
    }

    //Fixed update is called a certain amount of times per second and is unrelated to frames
    void FixedUpdate()
    {
        float moveInput = 0;
        
        
        //Move the player
       
        //else
        //{
        //    rb.velocity = new Vector2(0 * speed, rb.velocity.y);
        //}

        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (Input.GetKey(leftKey))
            {
                moveInput = -1;
            }
            else if (Input.GetKey(rightKey))
            {
                moveInput = 1;
            }
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        
        myAnimator.SetFloat("Speed", Mathf.Abs(moveInput));



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

        //These two methods play the attack, but stop it once it plays once. 

        HandleAttacks();
        resetAttack();

        
    }

    //If the attack button is pressed and therefore is true, this method will play the attack animation

 

    private void HandleAttacks()
    {
        if (attack)
        {
            myAnimator.SetTrigger("Attack");
        }
    }

    //This method determines if the attack button is being pressed at any one time


    private void inputAttack()
    {
        if (Input.GetKeyDown(attackKey))
        {
            attack = true;
        }

    }

    private void resetAttack()
    {
        attack = false;
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

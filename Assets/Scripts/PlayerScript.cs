using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The script that controls each player
 */
public class PlayerScript : MonoBehaviour
{
    public KeyCode leftKey; // the left movement input key
    public KeyCode rightKey; // the right movement input key
    public KeyCode jumpKey; // the jump input key
    public KeyCode attackKey; // the attack input key

    public float speed = 5f; // the movement speed
    public float jumpForce = 15f; // the force to apply when jumping
    private float moveInput = 0; // the direction represented as an int

    public bool isGrounded; // if the player is on a platform
    private bool facingRight; // if the player is facing right

    private Rigidbody2D rb; // the Rigidbody2D Component of the player
    private Animator myAnimator; // the Animator Component of the player

    public string playerName; // the name of the player

    public int startingScore = 0; // the score that the player starts with
    public int score = 0; // the current score of the player

    void Start()
    {
        // get the Rigidbody2D Component of this GameObject
        rb = GetComponent<Rigidbody2D>();

        // get the Animator Component of this GameObject
        myAnimator = GetComponent<Animator>();

        // set the GameObject's name to that of the player's
        this.name = playerName;
    }

    void Update()
    {
        UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        // set the speed of the animator to that of the direction, which can be 1, -1, or 0
        myAnimator.SetFloat("Speed", Mathf.Abs(moveInput));

        // if the attack button is pressed, play the attack animation
        if (Input.GetKeyDown(attackKey) && moveInput == 0)
        {
            // play the Attack animation
            myAnimator.SetTrigger("Attack");

            // push the other player
            PushOtherPlayer();
        }

        // flip the player, depending which direction they are moving
        if (!facingRight && moveInput < 0)
        {
            Flip();
        }
        else if (facingRight && moveInput > 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        // flip the player's sprite by swapping it's scale
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // check what key the player has / hasn't pressed, and set a representation of that direction as an int
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

        // set the current rigidbody's velocity to a new speed, calculated by the direction * speed, keeping the old y velocity
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // check if player has pressed the jump key, and can jump
        if (Input.GetKey(jumpKey) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void PushOtherPlayer()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, layerMask);
        Debug.DrawRay(transform.position, Vector2.right * 2.11f, Color.red, 20f);
    }

    public void SetName(string newName)
    {
        // set the current name to a new name
        playerName = newName;

        // set the GameObject's name to that of the player's
        this.name = playerName;
    }

    public void SetScore(int newScore)
    {
        // set the current score to a new score
        score = newScore;
    }

    public void IncrementScore()
    {
        // increase the current score
        score++;
    }

    public int GetScore()
    {
        // return the current score of the player
        return score;
    }

    public void CheckPlayerGrounded(Collider2D collider)
    {
        // check if the provided collider's is not an objective
        if (collider.tag != "Objective")
        {
            // set the player is grounded
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckPlayerGrounded(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        CheckPlayerGrounded(collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        isGrounded = false;
    }
}
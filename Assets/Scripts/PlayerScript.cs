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
    AudioSource shoot;

    public float speed = 7f; // the movement speed
    public float jumpForce = 20f; // the force to apply when jumping
    private float moveInput = 0; // the direction represented as an int

    public bool isGrounded; // if the player is on a platform
    private bool facingRight; // if the player is facing right

    public bool isInSpace = false;

    private Rigidbody2D rb; // the Rigidbody2D Component of the player
    private Animator myAnimator; // the Animator Component of the player

    public string playerName; // the name of the player

    public int startingScore = 0; // the score that the player starts with
    public int score = 0; // the current score of the player

    public Vector3 spawnPos;

    public LevelController levelController;

    void Start()
    {
        ResetPos();
        shoot = GetComponent<AudioSource>(); 

        // get the Rigidbody2D Component of this GameObject
        rb = GetComponent<Rigidbody2D>();

        UpdateGravity();

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
        if (Input.GetKeyDown(attackKey))
        {
            // play the Attack animation
            shoot.Play();
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
        CheckFallOff();
        UpdateGravity();
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
        rb.AddForce(new Vector2(moveInput * speed * 7f, rb.velocity.y));
        //        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // check if player has pressed the jump key, and can jump
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            // rb.velocity = Vector2.up * jumpForce;
            rb.AddForce(Vector2.up * jumpForce * 1200f);
        }
    }

    void UpdateGravity()
    {
        if (isInSpace)
        {
            rb.angularDrag = 0.6f;
            rb.gravityScale = 1f;
        }
        else
        {
            rb.angularDrag = 0.2f;
            rb.gravityScale = 5f;
        }
    }

    void PushOtherPlayer()
    {
        float dir = -1.0f;
        if (!facingRight)
        {
            dir = 1.0f;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, dir * 1.61f);
        if (hit.collider != null && hit.collider.name != this.name && hit.rigidbody != null)
        {
            if (hit.collider.name == "Foreground")
            {
                rb.AddForce(new Vector2(-30000f * dir, 10000f));
            }
            else
            {
                //Debug.Log(hit.collider.name);
                hit.rigidbody.AddForce(new Vector2(30000f * dir, 10000f)); // this breaks because the velocity is set in the Move() method. maybe change move to add to velocity?
            }
        }
        Debug.DrawRay(transform.position, Vector2.right * 1.61f * dir, Color.red, 10f);
    }

    public void SetName(string newName)
    {
        // set the current name to a new name
        playerName = newName;

        // set the GameObject's name to that of the player's
        this.name = playerName;
    }

    public string GetName()
    {
        return name;
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
        if (collider.tag != "Objective" && collider.tag != "Coin" && collider.tag != "Player" && collider.tag != "Player")
        {
            // set the player is grounded
            isGrounded = true;
            myAnimator.SetBool("Isgrounded", true);
        }
    }

    public void CheckForCoin(Collider2D collider)
    {
        if (collider.gameObject.tag == "Coin")
        {
            AudioSource sound = collider.GetComponent<AudioSource>();
            sound.Play();
        }
    }

    void CheckFallOff()
    {
        if (transform.position.y <= -50)
        {
            ResetPos();
        }
    }

    public void ResetPos()
    {
        transform.position = spawnPos;
    }

    public void Respawn()
    {
        levelController.Respawn();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckPlayerGrounded(collider);
        CheckForCoin(collider);
        CheckGravity(collider);
        CheckTeleporter(collider);
    }

    public void CheckTeleporter(Collider2D collider)
    {
        if (collider.gameObject.tag == "Teleporter")
        {
            transform.position = spawnPos;
        }
    }
    void CheckGravity(Collider2D collider)
    {
        // check if the provided collider's is not an objective
        if (collider.tag == "GravArea")
        {
            isInSpace = true;
        }
        else if (collider.tag == "GravAreaExit")
        {
            isInSpace = false;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        CheckPlayerGrounded(collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Coin" && collider.gameObject.tag != "Player")
        {
            isGrounded = false;
            myAnimator.SetBool("Isgrounded", false);
        }
    }
}
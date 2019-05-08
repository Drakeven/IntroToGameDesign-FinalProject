using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    
    public float speed;
    private bool canJump;
    public float jumpForce;
    private float moveInput;
    private Rigidbody2D rb;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fixed update is called a certain amount of times per second and is unrelated to frames
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(!facingRight && moveInput < 0)
        {
            Flip();
        }
        else if(facingRight && moveInput > 0)
        {
            Flip();
        }

        move();
    }

    void move()
    {
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * speed * Time.deltaTime);
            //transform.position += Vector3.left * speed * Time.deltaTime;
        }
        //Right
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.right * speed * Time.deltaTime);
            //transform.position += Vector3.right * speed * Time.deltaTime;
        }

        //If the player is touching the ground then jump
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce);
            //transform.position += Vector3.up * jumpForce * Time.deltaTime;
        }
        ////Down
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position += Vector3.down * speed * Time.deltaTime;
        //}

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Touching Ground");
        canJump = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Not Touching Ground");
        canJump = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

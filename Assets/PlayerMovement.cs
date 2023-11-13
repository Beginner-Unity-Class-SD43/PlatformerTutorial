using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float horizontal;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpPower = 16f;

    bool isFacingRight = true;

    Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    Animator anim;

    Vector2 respawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        respawnPoint = transform.position;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        
        // Change animation states
        if(horizontal != 0 && CheckIfGrounded())
        {
            anim.SetBool("run", true);
            anim.SetBool("jump", false);
            anim.SetBool("idle", false);
        }
        else if(!CheckIfGrounded() && rb.velocity.y != 0f)
        {
            anim.SetBool("jump", true);
            anim.SetBool("run", false);
            anim.SetBool("idle", false);
        }
        else if(CheckIfGrounded() && horizontal == 0)
        {
            anim.SetBool("idle", true);
            anim.SetBool("jump", false);
            anim.SetBool("run", false);
        }

        if(Input.GetButtonDown("Jump") && CheckIfGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            transform.position = respawnPoint;
        }

    }

    bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

}

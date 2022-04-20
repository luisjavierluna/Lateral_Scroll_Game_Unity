using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 10;
    [SerializeField] float raycastDistance = 1.5f;
    [SerializeField] LayerMask groundMask;

    const string HORIZONTAL = "Horizontal";
    [SerializeField] float speed = 10;
    float h;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw(HORIZONTAL);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        Move();


    }

    void Jump()
    {
        if (!IsJumping())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsJumping()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundMask))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
    }
}

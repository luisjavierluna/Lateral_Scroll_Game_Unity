using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 10;
    [SerializeField] float raycastDistance = 1.5f;
    [SerializeField] LayerMask groundMask;


    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

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
}

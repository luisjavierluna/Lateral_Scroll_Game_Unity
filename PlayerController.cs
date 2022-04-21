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

    const string IS_ALIVE = "IsAlive";
    const string IS_JUMPING = "IsJumping";
    const string IS_WALKING = "IsWalking";

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool(IS_ALIVE, true);
    }

    private void Update()
    {
        h = Input.GetAxisRaw(HORIZONTAL);

        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            Move();
            RenderDirection();
        }

        anim.SetBool(IS_JUMPING, IsJumping());
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

    void RenderDirection()
    {
        if (h > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        if (h < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        anim.SetBool(IS_WALKING, h != 0.0f);
    }

    public void Die()
    {
        anim.SetBool(IS_ALIVE, false);
        rb.velocity = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int enemyDamage = 10;
    [SerializeField] float speed = 5;
    [SerializeField] bool facingRight;

    PlayerController player;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        float currentSpeed = speed;
        if (facingRight)
        {
            currentSpeed = speed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentSpeed = -speed;
            transform.eulerAngles = Vector3.zero;
        }

        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            player.HealthControl(-enemyDamage);
            return;
        }
        facingRight = !facingRight;
    }
}

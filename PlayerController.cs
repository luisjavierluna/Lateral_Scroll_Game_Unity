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

    Vector2 initialPosition;

    [SerializeField] int healthPoints = INITIAL_HEALTH;
    [SerializeField] int manaPoints = INITIAL_MANA;

    public const int INITIAL_HEALTH = 100;
    public const int MAX_HEALTH = 200;
    public const int MIN_HEALTH = 0;
    public const int INITIAL_MANA = 100;
    public const int MAX_MANA = 200;
    public const int MIN_MANA = 0;

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool(IS_ALIVE, true);

        initialPosition = transform.position;
    }

    public void RestartPosition()
    {
        Invoke("InitialPosition", 0.3f);
    }

    void InitialPosition()
    {
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        anim.SetBool(IS_ALIVE , true);

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;
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
        GameManager.instance.GameOverState();
        anim.SetBool(IS_ALIVE, false);
        rb.velocity = Vector2.zero;

        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0);

        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
    }

    public void HealthControl(int points)
    {
        healthPoints += points;
        if (healthPoints > MAX_HEALTH)
        {
            healthPoints = MAX_HEALTH;
        }

        if (healthPoints <= MIN_HEALTH)
        {
            Die();
        }
    }

    public void ManaControl(int points)
    {
        manaPoints += points;
        if (manaPoints > MAX_MANA)
        {
            manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return transform.position.x - initialPosition.x;
    }
}

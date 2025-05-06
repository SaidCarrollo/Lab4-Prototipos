using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private PlayerScoreData scoreData;

    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Color defaultColor = Color.white;
    private int currentHealth;
    private int score;
    private int currentJumps;
    private Color currentColor;
    private bool isCollidingWithObstacle = false;
    private bool isGrounded;

    private Rigidbody2D rb;
    private RaycastHit2D groundCheck;

    [SerializeField] private GameBoolEvent onVictoryDefeat;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentColor = defaultColor;
        UpdatePlayerColor();
        UIEventManager.Instance.OnColorChange += ChangeColor;
    }

    private void OnDestroy()
    {
        // Importante: Desuscribirse para evitar memory leaks
        UIEventManager.Instance.OnColorChange -= ChangeColor;
    }
    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGrounded = groundCheck.collider != null;

        if (isGrounded)
        {
            currentJumps = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentJumps < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            currentJumps++;
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UIEventManager.Instance.OnDamageTaken?.Invoke(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UIEventManager.Instance.OnHeartCollected?.Invoke(amount);
    }

    public void AddScore(int points)
    {
        scoreData.AddScore(points);
        UIEventManager.Instance.OnCoinCollected?.Invoke(points);
    }

    public void ChangeColor(Color newColor)
    {
        if (!isCollidingWithObstacle)
        {
            currentColor = newColor;
            UpdatePlayerColor();
        }
    }

    private void UpdatePlayerColor()
    {
        if (playerSprite != null)
        {
            playerSprite.color = currentColor;
        }
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }
    private void Die()
    {
        onVictoryDefeat?.Raise(false);
        //SceneManager.LoadScene("Results");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            Collectable collectable = other.GetComponent<Collectable>();
            if (collectable != null)
            {
                collectable.Collect(this);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isCollidingWithObstacle = true;
        }
        if (collision.gameObject.CompareTag("Door"))
        {
            onVictoryDefeat?.Raise(true);
            //SceneManager.LoadScene("Results");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isCollidingWithObstacle = false;
        }
    }

}
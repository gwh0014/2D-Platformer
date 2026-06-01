using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    float movementSpeed = 5f;

    Rigidbody2D rb;

    float jumpForce = 5f;
    int maxJumps = 2;
    int jumpsRemaining;

    int coinCount = 0;

    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
            spriteRenderer.flipX = false;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
            spriteRenderer.flipX = true;
        }

        rb.linearVelocity = new Vector2(moveInput * movementSpeed, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Space"))
        {
            RestartLevel();
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("The Alien has caught you! Try again.");
            RestartLevel();
        }
        else if (other.CompareTag("Goal") && coinCount >= 2)
        {
            Debug.Log("You have successfully robbed the Alien!");
            RestartLevel();
        }
        else if (other.CompareTag("Coin"))
        {
            Debug.Log("You Stole a coin from the Alien");
            coinCount++;
            other.gameObject.SetActive(false);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        coinCount = 0;
    }
}

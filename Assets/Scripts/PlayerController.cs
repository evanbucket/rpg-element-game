using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerState {
        Idle,
        Moving,
        Attacking,
    }

    private PlayerState currentState = PlayerState.Idle;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private const int SPEED_UNIT = 1000;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentState = PlayerState.Idle;
    }

    void IdleState(Vector2 direction)
    {
        if (direction != new Vector2(0,0)) {
            currentState = PlayerState.Moving;
            // sr.color = Color.green;
            // Debug.Log("Time to go!");
        }
    }

    void MoveState(Vector2 direction)
    {
        if (direction == new Vector2(0,0)) {
            currentState = PlayerState.Idle;
            // sr.color = Color.red;
            // Debug.Log("I'm idle!");
        }
    }

    void FixedUpdate()
    {
        // +1 for right/up, -1 for left/down.
        // Example: Vector2(1, -1) = diagonal direction right and down.
        Vector2 direction = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        transform.position = new Vector2(
            transform.position.x + speed * direction.x / SPEED_UNIT,
            transform.position.y + speed * direction.y / SPEED_UNIT
        );

        // If current state is moving, the player is moving and is doing walking animation
        if (currentState == PlayerState.Moving) {
            MoveState(direction);
            /* animator.SetBool("isMoving", true); */

        // If current state is idle, the player is idle, and is not doing the walking animation
        } else if (currentState == PlayerState.Idle) {
            IdleState(direction);
            /* animator.SetBool("isMoving", false); */
        }

        // Flip sprite
        /* if (direction.x < 0) {
            this.transform.localScale = new Vector3(-0.529f, 0.427639f, 1);
        } else if (direction.x > 0) {
            this.transform.localScale = new Vector3(0.529f, 0.427639f, 1);
        }  */

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy") {
                // Respawn
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                /* transform.position = startLocation;
                Debug.Log("Reload the scene"); */
            }
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    public float xRange = 17.5f;
    public float yRange = 6.5f;

    public InputAction moveAction;
    private Vector2 moveInput;

    private Rigidbody playerRb;
    private readonly float tiltAngle = 15.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainPlayerPosition();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    // Moves the player based on WASD input
    private void MovePlayer()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        playerRb.linearVelocity = new Vector3(moveInput.x * speed, moveInput.y * speed, 0);

        // Tilt the spaceship when moving up or down
        float tilt = Mathf.Clamp(moveInput.y * tiltAngle, -tiltAngle, tiltAngle);
        transform.rotation = Quaternion.Euler(tilt, 0, 0);
    }

    // Prevent the player from moving outside the bounds of the screen
    private void ConstrainPlayerPosition()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
        else if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with an enemy.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}

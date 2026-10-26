using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    public float xRange = 17.5f;
    public float yRange = 6.5f;

    private readonly float tiltAngle = 15.0f;

    public GameObject rocketPrefab;
    public ParticleSystem explosionEffect;

    private InputSystem_Actions controls;

    public bool hasPowerup = false;
    public int lives = 1;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    // OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        controls.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Lives: " + lives);
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainPlayerPosition();

        // Fire rocket if attack button was pressed this frame 
        if (controls.Player.Attack.WasPressedThisFrame())
        {
            if (hasPowerup)
            {
                LaunchRocketWithSpread();
            }
            else
            {
                LaunchSingleRocket();
            }
        }
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void FixedUpdate()
    {
        MovePlayer();

    }

    // Moves the player based on WASD input
    private void MovePlayer()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        Vector3 move = new(moveInput.x, moveInput.y, 0);
        transform.Translate(speed * Time.deltaTime * move, Space.World);

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
            lives--;
            hasPowerup = false;
            Destroy(collision.gameObject);

            if (lives <= 0)
            {
                Debug.Log("Player has died.");
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Player has collided with an enemy.");
                Debug.Log("Lives: " + lives);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Debug.Log("Player has collected a powerup.");
            Destroy(other.gameObject);
            hasPowerup = true;
        }
    }

    // Launch a single rocket projectile
    private void LaunchSingleRocket()
    {
        // Instantiate a rocket prefab at the player's position and rotation
        GameObject rocket = Instantiate(rocketPrefab, transform.position + Vector3.right, Quaternion.identity);
        // Get the RocketBehaviour component of the instantiated rocket
        RocketBehaviour rocketBehaviour = rocket.GetComponent<RocketBehaviour>();
        // Set the direction of the rocket to the right
        rocketBehaviour.Direction = Vector3.right;
    }

    // Launch three rocket projectiles in a spread pattern
    private void LaunchRocketWithSpread()
    {
        // Instantiate three rocket prefabs at the player's position and rotation
        GameObject rocket1 = Instantiate(rocketPrefab, transform.position + Vector3.right, Quaternion.identity);
        GameObject rocket2 = Instantiate(rocketPrefab, transform.position + Vector3.right + Vector3.up, Quaternion.identity);
        GameObject rocket3 = Instantiate(rocketPrefab, transform.position + Vector3.right + Vector3.down, Quaternion.identity);

        // Get the RocketBehaviour components of the instantiated rockets
        RocketBehaviour rocketBehaviour1 = rocket1.GetComponent<RocketBehaviour>();
        RocketBehaviour rocketBehaviour2 = rocket2.GetComponent<RocketBehaviour>();
        RocketBehaviour rocketBehaviour3 = rocket3.GetComponent<RocketBehaviour>();

        // Set the directions of the rockets
        rocketBehaviour1.Direction = Vector3.right;
        rocketBehaviour2.Direction = Vector3.right;
        rocketBehaviour3.Direction = Vector3.right;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    public float xRange = 17.5f;
    public float yRange = 6.5f;

    private InputSystem_Actions controls;

    private readonly float tiltAngle = 15.0f;

    public GameObject rocketPrefab;

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
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainPlayerPosition();

        // Fire rocket if attack button was pressed this frame 
        if (controls.Player.Attack.WasPressedThisFrame())
        {
            LaunchRocket();
        }
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

    // Launch a rocket from the player's position
    private void LaunchRocket()
    {
        // Instantiate a rocket prefab at the player's position and rotation
        GameObject rocket = Instantiate(rocketPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        // Get the RocketBehaviour component of the instantiated rocket
        RocketBehaviour rocketBehaviour = rocket.GetComponent<RocketBehaviour>();
        // Fire the rocket in the forward direction of the player
        rocketBehaviour.Fire(new Vector3(1, 0, 0));
    }
}

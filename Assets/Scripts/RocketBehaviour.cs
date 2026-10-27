using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    public float speed = 30.0f;

    // ENCAPSULATION
    public Vector3 Direction { get; set; }

    private readonly float xDestroy = -22.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Move the rocket forward in the direction it was fired
        transform.Translate(speed * Time.deltaTime * Direction, Space.World);

        if (transform.position.x < xDestroy || transform.position.x > -xDestroy)
        {
            Destroy(gameObject);
        }
    }

    // Called when the rocket collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

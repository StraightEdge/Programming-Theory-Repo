using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private bool alive = false;

    public float speed = 30.0f;
    private Vector3 direction;

    private float xDestroy = -22.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            // Move the rocket forward in the direction it was fired
            transform.Translate(speed * Time.deltaTime * direction, Space.World);
        }

        if (transform.position.x < xDestroy || transform.position.x > -xDestroy)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Fires the rocket in the specified direction.
    /// </summary>
    /// <param name="direction">The direction to fire the rocket.</param>
    public void Fire(Vector3 dir)
    {
        alive = true;
        direction = dir.normalized;
    }
}

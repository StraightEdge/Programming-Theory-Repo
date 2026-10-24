using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;

    private readonly float xDestroy = -50.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DestroyOutOfBound();
    }

    // Move the enemy to the left at a constant speed
    protected virtual void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
    }

    // Destroy the enemy when it moves off the left side of the screen
    protected void DestroyOutOfBound()
    {
        if (transform.position.x < xDestroy)
        {
            Destroy(gameObject);
        }
    }
}

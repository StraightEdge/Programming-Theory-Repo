using UnityEngine;

public class EnemySpaceship : Enemy
{
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

    // Move the enemy spaceship to the left with a sine wave pattern 
    // with a constant speed, low frequency, and low amplitude
    protected override void Move()
    {
        float frequency = 2.0f; // Frequency of the sine wave
        float amplitude = 0.05f; // Amplitude of the sine wave
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude; // Calculate the y offset based on the sine wave

        // Move the spaceship to the left with a sine wave pattern
        transform.Translate(speed * Time.deltaTime * Vector3.left + new Vector3(0, yOffset, 0));
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;

    public void Initialize(float initialSpeed)
    {
        speed = initialSpeed;
    }

    private void Update()
    {
        // Move the projectile forward based on its speed
        transform.position = transform.position + transform.right * speed * Time.deltaTime;
    }
}

using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletLifetime = 2f;

    private void Start()
    {
        // Destroy the bullet after a certain amount of time
        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision was with a scene object
        if (collision.gameObject.CompareTag("CollisionObject"))
        {
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
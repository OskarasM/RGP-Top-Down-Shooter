using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownCharacterController : MonoBehaviour
{
    //Reference to attached animator
    private Animator animator;

    //Reference to attached rigidbody 2D
    private Rigidbody2D rb;

    //The direction the player is moving in
    private Vector2 playerDirection;

    //The speed at which they're moving
    private float playerSpeed = 1f;

    public GameObject bulletPrefab; // the prefab for the bullet game object
    public Transform firePoint; // the position where the bullet will be spawned
    public float bulletForce = 10f; // the speed at which the bullet will travel
    public float bulletLifetime = 2.0f;

    [Header("Movement parameters")]

    //The maximum speed the player can move
    [SerializeField] private float playerMaxSpeed = 100f;

    
    private void Awake()
    {
        //Get the attached components so we can use them later
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Set the velocity to the direction they're moving in, multiplied by the speed they're moving
        rb.velocity = playerDirection * (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime;
    }

    public void OnPlayerInputShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // create a new bullet object from the prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // use playerDirection as the direction vector for the bullet
        Vector2 direction = playerDirection.normalized;

        // apply force to the bullet in the playerDirection
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }

    /// <param name="context"></param>
    public void OnPlayerInputMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            //Was the action just cancelled (released)? If so, set
            //speed to 0
            playerSpeed = 0f;

            //Update the animator too, and return
            animator.SetFloat("Speed", 0);
            return;
        }

        //Otherwise, if the context wasn't performed, don't run the code below
        if (!context.performed)
            return;

        //Read the direction that the player wants to move, from the
        //keys that have been pressed
        Vector2 direction = context.ReadValue<Vector2>();

        //Set the player's direction to whatever it is
        playerDirection = direction;

        //Set animator parameters
        animator.SetFloat("Horizontal", playerDirection.x);
        animator.SetFloat("Vertical", playerDirection.y);
        animator.SetFloat("Speed", playerDirection.magnitude);

        //And set the speed to 1, so they move!
        playerSpeed = 1f;
    }
}
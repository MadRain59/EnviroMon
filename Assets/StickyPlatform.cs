using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Adjust player's physics properties (e.g., friction, gravity) to simulate stickiness
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Example: Set higher friction when on the sticky platform
                playerRb.sharedMaterial.friction = 1.0f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reset player's physics properties when leaving the sticky platform
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Example: Reset friction to its original value
                playerRb.sharedMaterial.friction = 0.4f; // Set it to the default friction value
            }
        }
    }
}

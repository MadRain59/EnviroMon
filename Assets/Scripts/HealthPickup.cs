using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 25;
    // Start is called before the first frame update
    void Start()
    {

    }

    //Script detects when player has collided with the healing game object and then it runs the Heal function and destroys the game object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable && damageable.Health < 100)
        {
            damageable.Heal(healthRestore);
            Destroy(gameObject);
        }
    }


}

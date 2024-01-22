using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 projectileMoveSpeed = new Vector2(5f, 0);
    public int damage = 35;
    public Vector2 knockback = new Vector2 (0, 0);

    Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //calculates the new position of the projectile by using Vector2 multiplied by the localscale of the projectile)
        rb.velocity = new Vector2(projectileMoveSpeed.x * transform.localScale.x, projectileMoveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null ) 
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0f ? knockback : new Vector2(-knockback.x, knockback.y);
            //Hit target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            if (gotHit) 
            {
                Destroy(gameObject);
            }
        }
    }
}

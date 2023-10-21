using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackColl;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        attackColl = GetComponent<PolygonCollider2D>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if a gameobject is damageable/can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null) 
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            //Hit target
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (gotHit)
            Debug.Log(collision.name + "hit for " + attackDamage);
        }

    }
}

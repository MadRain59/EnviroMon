using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TouchingDirections), typeof(Damageable))]
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed at which the enemy moves
    private Transform player; // Reference to the player's Transform
    private bool facingRight = true; // Flag to track the enemy's facing direction

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator anim;
    Damageable damageable;

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            anim.SetBool("hasTarget", value);
        }
    }

    public bool CanMove
    {
        get
        {
            return anim.GetBool("canMove");
        }
    }

    public float AttackCD
    {
        //get float value of attackCD
        get
        {
            return anim.GetFloat("attackCooldown");
        }

        //reads the current value of attackCD, if attackCD has passsed knight can attack again
        private set
        {
            //("string", Mathf.Max(value, 0)), to make sure the number never counts below 0
            anim.SetFloat("attackCooldown", Mathf.Max(value, 0));
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

        // Find the player object using its tag (you can adjust this depending on how your player is set up)
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (AttackCD > 0)
        {
            //counts the old time and subtracts it with the current time to get current value
            AttackCD -= Time.deltaTime;
        }

        if (player != null)
        {
            // Get the direction from the enemy to the player
            Vector3 direction = player.position - transform.position;

            // Normalize the direction vector to have a magnitude of 1
            direction.Normalize();

            // Move towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Flip the enemy if necessary
            Flip(direction.x);
        }
    }

    private void Flip(float directionX)
    {
        bool attackCDCheck = anim.GetFloat("attackCooldown") == 0;

        if ((directionX > 0 && !facingRight) || (directionX < 0 && facingRight) && attackCDCheck)
        {
            // Change the facing direction
            facingRight = !facingRight;

            // Reverse the scale in the X axis to change its orientation
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }


    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}

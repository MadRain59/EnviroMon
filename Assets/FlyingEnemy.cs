using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 2f;
    private GameObject player;
    public bool chase = false;

    Animator anim;
    Rigidbody rb;
    Damageable damageable;
    public DetectionZone biteDetectionZone;

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

    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponent<Damageable>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if (chase == true)
            Chase();
        Flip();
        Die();

        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void Chase()
    { 
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); 
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void Die()
    {
        if (damageable.IsAlive == false)
        {
            Destroy(gameObject);

        }
    }
}

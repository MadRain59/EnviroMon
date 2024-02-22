using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;

    Animator anim;

    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField] private int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if (_health <= 0)
            {
                IsAlive = false;
                Debug.Log("Character is no longer alive");
            }
        }
    }

    [SerializeField] private bool _isAlive = true;

    [SerializeField] private bool isInvincible = false;

    public bool IsHit
    {
        get { return anim.GetBool("IsHit"); }
        private set { anim.SetBool("IsHit", value); }
    }

    private float timeSinceHit = 0f;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            anim.SetBool("IsAlive", value);
            Debug.Log("IsAlive set " + value);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            IsHit = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public void Heal(int healthRestore)
    {
        if (IsAlive && Health < 100)
        {
            Health += healthRestore;
            CharacterEvents.characterHealed.Invoke(gameObject, healthRestore);
        }
    }
}

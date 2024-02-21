using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    public GameObject coinsCounterer;
    CoinsCounter coinsCounter;

    Animator anim;
    
    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField] private int _health = 100;

    public int Health
    { 
        get 
        { 
            return _health; 
        } 
        set 
        { 
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if(_health <= 0)
            {
                IsAlive = false;
                coinsCounter.coins += 1;
                Debug.Log("CoinsAdded");
            }
        }
    }

    [SerializeField] private bool _isAlive = true;

    [SerializeField] private bool isInvincible = false;

    public bool IsHit
    {
        get
        {
            return anim.GetBool("IsHit");
        }
        private set 
        {
            anim.SetBool("IsHit", value);
        }
    }

    private float timeSinceHit = 0f;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            anim.SetBool("IsAlive", value);
            //checking on console whether or not character is taking damage
            Debug.Log("IsAlive set " + value);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coinsCounter = coinsCounterer.GetComponent<CoinsCounter>();

    }

    private void Update()
    {
        if (isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                //removes invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }
            //calculates the time since hit
            timeSinceHit += Time.deltaTime;
        }
    }
    //Returns whether or not the person took a hit or not
    public bool Hit(int damage, Vector2 knockback)
    {
        //detects when player IsAlive && is not isInvincible, only then the player takes damage
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            //basically calls the other components that the player was hit and handles knockback
            //? is to check whether or not damageableHit event is null or not and returns
            IsHit = true;
            damageableHit?.Invoke(damage, knockback);
            //Calls upon CharacterEvents.characterDamaged and Invokes it and shows the value of damage on the gameObject
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public void Heal(int healthRestore)
    {
        if(IsAlive && Health < 100) 
        {
            Health += healthRestore;
            //call on the CharacterEvents script to bring the characterHealed script and pass it through the game Object
            CharacterEvents.characterHealed(gameObject, healthRestore);
        }
    }
}

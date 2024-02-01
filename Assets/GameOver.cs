using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    Damageable damageable;
    public GameObject player;
    public GameObject deathUI;
    // Start is called before the first frame update
    void Start()
    {
        damageable = player.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player && damageable)
        {
            if (damageable.Health <= 0)
            {
                deathUI.SetActive(true);
            } 
        }
    }

}

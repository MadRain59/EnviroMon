using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseScript : MonoBehaviour
{
    public Enemy[] enemyArray;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Enemy enemy in enemyArray)
            {     
                enemy.chase = true;
                Debug.Log("Chasing");
                
            }
        }
    }

  
}

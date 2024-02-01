using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseScript : MonoBehaviour
{
    public Enemy[] enemyArray;
    // Start is called before the first frame update

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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{
    public FlyingEnemy[] enemyArray;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemyArray) 
          
            if (IsAttacking(enemy))
            {
                enemy.chase = false;
            }
        }
    }

    private bool IsAttacking(FlyingEnemy enemy)
    {
        Animator animator = enemy.GetComponent<Animator>();
        
        if (animator != null) 
        {
            bool isInAttackAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
            return isInAttackAnimation;
        }

        return false;
    }
}

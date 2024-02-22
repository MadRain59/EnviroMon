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
                if (!IsAttacking(enemy))
                {
                    enemy.chase = true;
                    Debug.Log("Chasing");
                }
                else
                {
                    enemy.chase = false;
                    Debug.Log("Stopping chase during Attack sub-state");
                }
            }
        }
    }

    private bool IsAttacking(Enemy enemy)
    {
        Animator animator = enemy.GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            bool isInAttackAnimation = currentState.IsName("Boss_Attack") && currentState.IsTag("Attack");
            return isInAttackAnimation;
        }

        return false;
    }
}

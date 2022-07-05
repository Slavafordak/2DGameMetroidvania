using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator MyAnimator;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask enemyLayers;
    public int AttackDamage = 40;
    public float AttackRate = 2f;
    float NextAttackTime = 0f;
    void Update()
    {
        if(Time.time >= NextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                FindObjectOfType<AudioManager>().Play("AttackPL");
                Attack();
                NextAttackTime = Time.time + 1f / AttackRate;
            }
        }
        
    }

    void Attack()
    {
        MyAnimator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
            FindObjectOfType<AudioManager>().Play("SK_HIT");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; 
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}

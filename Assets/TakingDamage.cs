using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : MonoBehaviour
{
    public Animator MyAnimator;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask enemyLayers;
    public int AttackDamage = 40;
    void Update()
    {

    }

    void Attack()
    {
        MyAnimator.SetTrigger("Attack");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);

        foreach (Collider2D Player in hitPlayer)
        {
            Player.GetComponent<Player>().TakeDamage(AttackDamage);
        }
    }
}

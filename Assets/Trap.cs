using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public int AttackDamage = 20;
    public LayerMask enemyLayers;
    void OnTriggerEnter2D(Collider2D other)
    {

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);

        foreach (Collider2D Player in hitPlayer)
        {
            FindObjectOfType<AudioManager>().Play("Hurt_PL");
            if (tag == "Trap")
            {
                Player.GetComponent<Player>().TakeDamage(AttackDamage);
            }
        }

    }


}

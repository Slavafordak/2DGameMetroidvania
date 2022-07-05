using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public int MaxHealth = 100;
    public int CurrentHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;

    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("SK_HIT"); 
        CurrentHealth -= damage;

        animator.SetTrigger("Hurt"); 

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        FindObjectOfType<AudioManager>().Play("SK_DESTR");

        animator.SetBool("IsDead",true);

        rb.gravityScale = 0;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    void Destroy()
    {
        Destroy(gameObject, 0);
    }


}

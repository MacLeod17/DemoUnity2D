using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    [HideInInspector] public float currentHealth;

    Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Play Hurt Animation
        animator.SetTrigger("Hurt");
        AudioManager.Instance.PlayAudio(1);

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{name} Died!");

        //Die Animation
        animator.SetTrigger("Death");

        //Disable Character
        if (gameObject.tag.Equals("Player"))
        {
            GetComponent<Player>().m_isDead = true;
        }
        else if (gameObject.tag.Equals("Enemy"))
        {
            GetComponent<Enemy>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
        }
        this.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_speed = 4.2f;

    public Transform attackPoint;
    public LayerMask[] attackLayers;

    public float attackRange = 0.5f;
    public float attackDamage = 40.0f;
    public float attackRate = 2f;
    public float moveRate = 2f;

    float attackCounter = 0f;
    float moveCounter = 0f;

    Animator animator;
    Rigidbody2D body2d;

    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();

        moveCounter = moveRate;
    }

    void Update()
    {
        attackCounter -= Time.deltaTime;
        moveCounter -= Time.deltaTime;

        body2d.velocity = new Vector2(m_speed, body2d.velocity.y);
        if (Mathf.Abs(m_speed) > Mathf.Epsilon) animator.SetInteger("AnimState", 2);

        if (attackCounter <= 0)
        {
            Attack();
            attackCounter = attackRate;
        }

        if (moveCounter <= 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
            m_speed *= -1.0f;
            moveCounter = moveRate;
        }
    }

    public void Attack()
    {
        // Play Attack Animation
        animator.SetTrigger("Attack");

        //Detect Player In Range Of Attack
        foreach (var layer in attackLayers)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layer);

            //Damage Them
            foreach (var player in hitEnemies)
            {
                player.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

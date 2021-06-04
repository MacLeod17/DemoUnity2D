using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculus : MonoBehaviour
{
    public float m_speed = 4.2f;

    public LayerMask[] attackLayers;
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public float attackDamage = 40.0f;
    public Vector2 attackRate = new Vector2(1, 10);
    public float moveRate = 2f;

    float currentAttackRate = 2f;
    float attackCounter = 0f;
    float moveCounter = 0f;

    Rigidbody2D body2d;

    void Start()
    {
        body2d = GetComponentInParent<Rigidbody2D>();

        currentAttackRate = Random.Range(attackRate.x, attackRate.y);
        moveCounter = moveRate;
    }

    void Update()
    {
        attackCounter -= Time.deltaTime;
        moveCounter -= Time.deltaTime;

        body2d.velocity = new Vector2(m_speed, body2d.velocity.y);

        if (attackCounter <= 0)
        {
            Attack();
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

        //Detect Player In Range Of Attack
        foreach (var layer in attackLayers)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layer);

            //Damage Them
            foreach (var player in hitEnemies)
            {
                player.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
                currentAttackRate = Random.Range(attackRate.x, attackRate.y);
                attackCounter = currentAttackRate;
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

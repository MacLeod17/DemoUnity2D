using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask[] enemyLayers;

    public float attackRange = 0.5f;
    public float attackDamage = 40.0f;
    public float attackRate = 2f;

    float attackCounter = 0f;

    Animator animator;
    Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        attackCounter -= Time.deltaTime;

        if (attackCounter <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
                attackCounter = attackRate;
            }
        }

        //Change between idle and combat idle
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.m_combatIdle = !player.m_combatIdle;
        }
    }

    public void Attack()
    {
        // Play Attack Animation
        animator.SetTrigger("Attack");
        AudioManager.Instance.PlayAudio(0);

        //Detect Enemies In Range Of Attack
        foreach (var layer in enemyLayers)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layer);

            //Damage Them
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

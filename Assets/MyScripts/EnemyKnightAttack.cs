using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnightAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public int attackDamage = 20;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public LayerMask playerLayer;
    EnemyRangeCheck enemyRange;
    Enemy thisEnemy;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyRange = attackPoint.GetComponent<EnemyRangeCheck>();
        thisEnemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && enemyRange.playerInRange && !thisEnemy.isDead)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

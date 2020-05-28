using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizardAttack : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 2.5f;
    public float attackRate = 0.5f;
    public GameObject spell;
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
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        LayerMask groundMask = LayerMask.GetMask("Ground");
        foreach (var player in hitPlayer)
        {
            if (player.CompareTag("Player"))
            {
                RaycastHit2D los = Physics2D.Linecast(attackPoint.transform.position, player.GetComponent<Transform>().position, groundMask);
                if (!los.collider || (los.collider.CompareTag("Platform") && !los.collider.CompareTag("Ground")))
                {
                    animator.SetTrigger("Attack");
                    spell.transform.position = new Vector2(attackPoint.transform.position.x, attackPoint.transform.position.y);
                    Instantiate(spell);
                }
                else if (los.collider)
                {

                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

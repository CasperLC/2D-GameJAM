using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public int attackDamage = 35;
    public float attackRange = 0.6f;
    public float attackRate = 3;
    float nextAttackTime = 0f;
    float nextUseTime = 0f;
    float useRate = 5f;
    public LayerMask enemyLayers;
    public LayerMask activateableLayer;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started Player Combat Script");
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextUseTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseActivateable();
                nextUseTime = Time.time + 1f / useRate;
            }
        }

    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    void UseActivateable()
    {
        Collider2D[] hitActivateables = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, activateableLayer);

        foreach (Collider2D activateable in hitActivateables)
        {
            if (activateable.CompareTag("Activateable"))
            {
                LeverScript lever = activateable.GetComponent<LeverScript>();
                if (lever.isEnabled)
                {
                    lever.LeverOff();
                }
                else if (!lever.isEnabled)
                {
                    lever.LeverOn();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

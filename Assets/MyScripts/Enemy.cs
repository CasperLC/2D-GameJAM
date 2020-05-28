using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public bool isDead;
    public GameObject healthPickUp;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        isDead = true;

        
        // Health pickup
        healthPickUp.transform.position = new Vector2(transform.position.x, transform.position.y);
        Instantiate(healthPickUp);


        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<CapsuleCollider2D>().enabled = false;

        this.gameObject.tag = "Dead";
        

    }
}

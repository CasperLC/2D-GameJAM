using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    GroundChecker groundChecker;
    SideCheckerRight rightChecker;
    SideCheckerLeft leftChecker;
    public LayerMask groundLayer;
    private Vector2 lastPos;
    private bool isRight = true;
    

    //Health
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead;

    //Movement fields
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float moveSpeed = 4f;

    public UIController uiController;
    public BoxCollider2D groundCheckBox;
    public BoxCollider2D rightCheckerBox;
    public BoxCollider2D leftCheckerBox;

    public bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundChecker = groundCheckBox.GetComponent<GroundChecker>();
        rightChecker = rightCheckerBox.GetComponent<SideCheckerRight>();
        leftChecker = leftCheckerBox.GetComponent<SideCheckerLeft>();

        currentHealth = maxHealth;
        SetHealth(currentHealth, maxHealth);
        
        lastPos = rb2d.velocity;

    }

    void SetHealth(int current, int max)
    {
        if (current < 0)
        {
            current = 0;
        }
        uiController.health = current;
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (isRight)
            {
                if ((Input.GetKey("d") || Input.GetKey("right")) && !rightChecker.isBlockedByGround)
                {

                    rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                    isRight = true; //spriteRenderer.flipX = false;
                    if (rb2d.velocity.x != lastPos.x)
                        animator.SetBool("isMoving", true);
                }
                else if ((Input.GetKey("a") || Input.GetKey("left")) && !leftChecker.isBlockedByGround)
                {
                    rb2d.velocity = new Vector2(-1 * moveSpeed, rb2d.velocity.y);
                    isRight = false; //spriteRenderer.flipX = true;
                    animator.SetBool("isMoving", true);

                }
                else
                {
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                    animator.SetBool("isMoving", false);
                }
            }
            else if (!isRight)
            {
                if ((Input.GetKey("d") || Input.GetKey("right")) && !leftChecker.isBlockedByGround)
                {

                    rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                    isRight = true; //spriteRenderer.flipX = false;
                    if (rb2d.velocity.x != lastPos.x)
                        animator.SetBool("isMoving", true);
                }
                else if ((Input.GetKey("a") || Input.GetKey("left")) && !rightChecker.isBlockedByGround)
                {
                    rb2d.velocity = new Vector2(-1 * moveSpeed, rb2d.velocity.y);
                    isRight = false; //spriteRenderer.flipX = true;
                    animator.SetBool("isMoving", true);

                }
                else
                {
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                    animator.SetBool("isMoving", false);
                }
            }


            if ((Input.GetKey("space") || Input.GetKey("w")) && groundChecker.isGrounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                rb2d.AddForce(new Vector2(0f, jumpForce));
                groundChecker.isGrounded = false;
                //= new Vector2(rb2d.velocity.x, 6);
            }

            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (isRight == false)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (!groundChecker.isGrounded)
            {

                if (rb2d.velocity.y > 0)
                {
                    animator.SetBool("isJumping", true);
                    animator.SetInteger("State", 1);
                }
                else
                {
                    animator.SetBool("isJumping", false);
                    animator.SetInteger("State", 2);
                }
            }
            else
            {
                animator.SetBool("isJumping", false);
                animator.SetInteger("State", 0);
            }
        }   
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        } else
        {
            animator.SetTrigger("Hit");
        }  
    }

    public void HealDamage(int heal)
    {
        if(currentHealth + heal <= maxHealth)
        {
            currentHealth += heal;
        } 
        else if(currentHealth + heal > maxHealth)
        {
            currentHealth = maxHealth;
        }
        SetHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        isDead = true;

        GetComponent<Rigidbody2D>().isKinematic = true;
        rb2d.velocity = new Vector2(0f, 0f);
    }


    // Update is called once per frame
    void Update()
    {

    }
}

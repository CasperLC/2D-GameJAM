using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public CircleCollider2D aggroCollider;
    public BoxCollider2D sideCheckerRight;
    public BoxCollider2D sideCheckerLeft;
    public BoxCollider2D groundCheckBox;
    public CapsuleCollider2D avoidPlayerCapsule;
    public GameObject enemy;
    public Animator animator;
    public Transform patrolChecker;
    public Transform attackPoint;
    private bool isLeft = true;

    Enemy thisEnemy;
    SideCheckerRight rightChecker;
    SideCheckerLeft leftChecker;
    GroundChecker groundChecker;
    EnemyAvoidPlayerBlock avoidPlayerChecker;
    EnemyRangeCheck enemyRange;

    EnemyAggro enemyAggro;
    public float speed = 3f;
    public float jumpForce = 8000f;
    public float nextWaypointDistance = 3f;
    private Vector3 initialPosition;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyAggro = aggroCollider.GetComponent<EnemyAggro>();
        rightChecker = sideCheckerRight.GetComponent<SideCheckerRight>();
        leftChecker = sideCheckerLeft.GetComponent<SideCheckerLeft>();
        groundChecker = groundCheckBox.GetComponent<GroundChecker>();
        avoidPlayerChecker = avoidPlayerCapsule.GetComponent<EnemyAvoidPlayerBlock>();
        thisEnemy = enemy.GetComponent<Enemy>();
        enemyRange = attackPoint.GetComponent<EnemyRangeCheck>();

    }

    private void FixedUpdate()
    {
        if (!thisEnemy.isDead)
        {
            MoveTowardsPlayer();
        }else
        {
            rb2d.velocity = new Vector2(0f, 0f);
            StartCoroutine(DestoryDead());
        }    
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DestoryDead()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    void MoveTowardsPlayer()
    {
        if (!avoidPlayerChecker.avoidPlayer)
        {
            if (enemyAggro.playerAggro && !enemyRange.playerInRange)
            {
                if (isLeft)
                {
                    if ((rightChecker.isBlockedByGround || leftChecker.isBlockedByGround) && groundChecker.isGrounded)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                        rb2d.AddForce(new Vector2(0f, jumpForce));
                        animator.SetBool("isMoving", false);
                        groundChecker.isGrounded = false;
                    }
                    else if (target.position.x > transform.position.x && !rightChecker.isBlockedByGround)
                    {
                        isLeft = false;
                        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                        if (groundChecker.isGrounded)
                        {
                            animator.SetBool("isMoving", true);
                        }

                    }
                    else if (target.position.x < transform.position.x && !leftChecker.isBlockedByGround)
                    {
                        isLeft = true;
                        rb2d.velocity = new Vector2(speed * -1, rb2d.velocity.y);
                        if (groundChecker.isGrounded)
                        {
                            animator.SetBool("isMoving", true);
                        }
                    }
                } else if (!isLeft)
                {
                    if ((rightChecker.isBlockedByGround || leftChecker.isBlockedByGround) && groundChecker.isGrounded)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                        rb2d.AddForce(new Vector2(0f, jumpForce));
                        groundChecker.isGrounded = false;
                    }
                    else if (target.position.x > transform.position.x && !leftChecker.isBlockedByGround)
                    {
                        isLeft = false;
                        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                        if (groundChecker.isGrounded)
                        {
                            animator.SetBool("isMoving", true);
                        }

                    }
                    else if (target.position.x < transform.position.x && !rightChecker.isBlockedByGround)
                    {
                        isLeft = true;
                        rb2d.velocity = new Vector2(speed * -1, rb2d.velocity.y);
                        if (groundChecker.isGrounded)
                        {
                            animator.SetBool("isMoving", true);
                        }
                    }
                }
            } 
            else if (enemyAggro.playerAggro && enemyRange.playerInRange)
            {
                rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
                if (rb2d.velocity.x < 0.1)
                {
                    animator.SetBool("isMoving", false);
                }
            }
            else
            {
                Patrolling();
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            if (rb2d.velocity.x < 0.1)
            {
                animator.SetBool("isMoving", false);
            }
            
        }

        if (isLeft == true)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (isLeft == false)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


    }

    void Patrolling()
    {
        animator.SetBool("isMoving", true);
        LayerMask groundMask = LayerMask.GetMask("Ground");
        LayerMask enemyMask = LayerMask.GetMask("Enemy");
        if (isLeft)
        {
            
            RaycastHit2D patrolGroundInfo = Physics2D.Raycast(patrolChecker.position, Vector2.down, 1f, groundMask);
            RaycastHit2D patrolWallInfo = Physics2D.Raycast(patrolChecker.position, Vector2.left, 0.5f, groundMask);
            RaycastHit2D patrolEnemyInfo = Physics2D.Raycast(patrolChecker.position, Vector2.left, 0.5f, enemyMask);
            if (patrolGroundInfo.collider && !patrolGroundInfo.collider.name.Equals("EnemyAggro") && !patrolGroundInfo.collider.name.Equals("AttackPoint"))
            {
                rb2d.velocity = new Vector2(speed * -1, rb2d.velocity.y);
                Debug.Log(patrolGroundInfo.collider.name);
            }
            if (patrolGroundInfo.collider == false || patrolWallInfo.collider || patrolEnemyInfo.collider)
            {
                Debug.Log("No collision");
                isLeft = false;
            }
            
        }
        else if (!isLeft)
        {
            rb2d.velocity = new Vector2(speed * 1, rb2d.velocity.y);
            RaycastHit2D patrolGroundInfo = Physics2D.Raycast(patrolChecker.position, Vector2.down, 1f, groundMask);
            RaycastHit2D patrolWallInfo = Physics2D.Raycast(patrolChecker.position, Vector2.right, 0.5f, groundMask);
            RaycastHit2D patrolEnemyInfo = Physics2D.Raycast(patrolChecker.position, Vector2.right, 0.5f, enemyMask);
            if (patrolGroundInfo.collider && !patrolGroundInfo.collider.name.Equals("EnemyAggro") && !patrolGroundInfo.collider.name.Equals("AttackPoint"))
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                Debug.Log(patrolGroundInfo.collider.name);
            }
            if (patrolGroundInfo.collider == false || patrolWallInfo.collider || patrolEnemyInfo.collider)
            {
                isLeft = true;
            }
        }
    }

}

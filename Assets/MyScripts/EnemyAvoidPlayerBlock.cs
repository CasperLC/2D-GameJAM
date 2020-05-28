using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvoidPlayerBlock : MonoBehaviour
{
    CapsuleCollider2D avoidanceRange;
    public bool avoidPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        avoidanceRange = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            avoidPlayer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            avoidPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            avoidPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

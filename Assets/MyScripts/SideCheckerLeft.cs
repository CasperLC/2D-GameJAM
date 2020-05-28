using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCheckerLeft : MonoBehaviour
{
    public bool isBlockedByGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy") || collision.CompareTag("Platform"))
        {
            isBlockedByGround = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy") || collision.CompareTag("Platform"))
        {
            isBlockedByGround = true;
        }
        else
        {
            isBlockedByGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy") || collision.CompareTag("Platform"))
        {
            isBlockedByGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

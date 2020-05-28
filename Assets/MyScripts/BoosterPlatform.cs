using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPlatform : MonoBehaviour
{

    public float boostForce = 10f;

    private PolygonCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x,boostForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

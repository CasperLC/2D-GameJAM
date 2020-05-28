using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int fireballDamage = 35;
    public GameObject fbExplosion;
    private float fireballSpeed = 1f;
    private Rigidbody2D rb2d;
    private Vector3 start;
    private Transform fireballTarget;

    // Start is called before the first frame update
    void Start()
    {
        fireballTarget = GameObject.Find("Player").transform;

        rb2d = GetComponent<Rigidbody2D>();
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.right = fireballTarget.position - transform.position;
        rb2d.AddForce((fireballTarget.position - start) * (fireballSpeed), ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        rb2d.AddForce((fireballTarget.position - start) * (fireballSpeed), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(fireballDamage);
            fbExplosion.transform.position = new Vector2(transform.position.x, transform.position.y);
            Destroy(gameObject);
            Instantiate(fbExplosion);
        }

        if (collision.CompareTag("Ground"))
        {
            fbExplosion.transform.position = new Vector2(transform.position.x, transform.position.y);
            Destroy(gameObject);
            Instantiate(fbExplosion);
        }
    }


}

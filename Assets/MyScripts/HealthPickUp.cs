using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int health = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered health trigger");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Inside IfStatement of HealthPickup");
            Player player = collision.GetComponent<Player>();
            player.HealDamage(health);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackRange : MonoBehaviour
{
    private PolygonCollider2D meleeRange;

    // Start is called before the first frame update
    void Start()
    {
        meleeRange = GetComponent<PolygonCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

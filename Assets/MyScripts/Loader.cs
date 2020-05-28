using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void AWake()
    {
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}

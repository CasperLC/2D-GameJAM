using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{

    private List<GameObject> levers;
    public EndingDoor endDoor;
    // Start is called before the first frame update
    void Start()
    {
        levers = AddChildObjects(transform, "Activateable");

        foreach (var lever in levers)
        {
            Debug.Log(lever.name + "was found in list");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckAllLeversEnabled()
    {
        int count = 0;
        foreach (GameObject lever in levers)
        {
            LeverScript ls = lever.GetComponent<LeverScript>();
            if (ls.isEnabled)
            {
                count++;
            }
        }
        Debug.Log("count = " + count + " / Levers count = " + levers.Count);
        if(count == levers.Count)
        {
            endDoor.DoorOpen();
        }
    }

    private List<GameObject> AddChildObjects(Transform parent, string tag)
    {
        List<GameObject> allLevers = new List<GameObject>();

        foreach (Transform child in parent)
        {
            if(child.gameObject.tag == tag)
            {
                allLevers.Add(child.gameObject);
            }
        }

        return allLevers;
    }
}

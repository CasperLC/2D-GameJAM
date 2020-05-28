using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDoor : MonoBehaviour
{

    private SpriteRenderer renderer;
    private Sprite openSprite, closedSprite;
    public Texture2D texture;
    private float baseTransformY;
    private float baseTransformX;
    public bool isEnabled = false;
    public UIController uiController;
    private BoxCollider2D endTrigger;
    Scene thisScene;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {

        thisScene = SceneManager.GetActiveScene();
        sceneName = thisScene.name;
        endTrigger = GetComponent<BoxCollider2D>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(texture.name);

        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name.Equals("Medieval_props_free_1"))
            {
                closedSprite = sprites[i];
            }
            if (sprites[i].name.Equals("Medieval_props_free_2"))
            {
                openSprite = sprites[i];
            }
        }

        baseTransformY = transform.position.y;
        baseTransformX = transform.position.x;
        renderer = GetComponent<SpriteRenderer>();

    }

    public void DoorClosed()
    {
        renderer.sprite = closedSprite;
        transform.position = new Vector2(baseTransformX, baseTransformY);
        isEnabled = false;
    }

    public void DoorOpen()
    {
        renderer.sprite = openSprite;
        transform.position = new Vector2(baseTransformX, baseTransformY);
        isEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isEnabled)
        {
            Debug.Log("Triggered the next level");
            if (sceneName == "Level_1")
            {
                SceneManager.LoadScene("Level_2");
            } else if(sceneName == "Level_2")
            {
                SceneManager.LoadScene("Level_3");
            } else if (sceneName == "Level_3")
            {
                SceneManager.LoadScene("Level_4");
            } else if (sceneName == "Level_4")
            {
                uiController.beatGame = true;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

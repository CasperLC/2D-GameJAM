using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    private SpriteRenderer renderer;
    private Sprite onSprite, offSprite;
    public Texture2D texture;
    public LeverController lc;
    private float baseTransformY;
    private float baseTransformX;
    public bool isEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(texture.name);

        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name.Equals("Medieval_props_free_11"))
            {
                offSprite = sprites[i];
            }
            if (sprites[i].name.Equals("Medieval_props_free_12"))
            {
                onSprite = sprites[i];
            }
        }

        baseTransformY = transform.position.y;
        baseTransformX = transform.position.x;
        renderer = GetComponent<SpriteRenderer>();

        LeverOff();
    }

    public void LeverOff()
    {
        renderer.sprite = offSprite;
        transform.position = new Vector2(baseTransformX, baseTransformY);
        isEnabled = false;
        lc.CheckAllLeversEnabled();
    }

    public void LeverOn()
    {
        renderer.sprite = onSprite;
        transform.position = new Vector2(baseTransformX - 0.1f, baseTransformY - 0.1f);
        isEnabled = true;
        lc.CheckAllLeversEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

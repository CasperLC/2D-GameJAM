using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public float startScreenDelay = 3f;

    private Text levelText;
    private GameObject startImage;
    private GameObject gameOverImage;
    public GameObject playerObject;
    public Player player;

    public int score;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        StartScreen();
    }

    void StartScreen()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        startImage = GameObject.Find("StartImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        gameOverImage = GameObject.Find("GameOverImage");
        gameOverImage.SetActive(false);
        playerObject.SetActive(false);
        startImage.SetActive(true);
        Invoke("HideStartImage", startScreenDelay);
    }

    void HideStartImage()
    {
        startImage.SetActive(false);
        playerObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead)
        {
            Invoke("GameOver", startScreenDelay);
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    void GameOver()
    {
        gameOverImage.SetActive(true);
        playerObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public float startScreenDelay = 3f;

    private Text levelText;
    private GameObject startImage;
    private GameObject gameOverImage;
    private GameObject winImage;
    public GameObject playerObject;
    public Player player;
    Scene thisScene;
    string sceneName;
    public bool beatGame = false;

    //Health
    public int health = 100;
    public int numOfHearts = 5;
    public Image[] hearts;
    public Sprite fullHeart;

    public int score;
    // Start is called before the first frame update
    void Awake()
    {
        thisScene = SceneManager.GetActiveScene();
        sceneName = thisScene.name;
        StartScreen();
    }



    void StartScreen()
    {
        playerObject = GameObject.Find("Player");
        player = player.GetComponent<Player>();
        startImage = GameObject.Find("StartImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = ""+sceneName;
        gameOverImage = GameObject.Find("GameOverImage");
        gameOverImage.SetActive(false);
        winImage = GameObject.Find("WinImage");
        winImage.SetActive(false);
        playerObject.SetActive(false);
        startImage.SetActive(true);
        Invoke("HideStartImage", startScreenDelay);
    }

    void HideStartImage()
    {
        startImage.SetActive(false);
        playerObject.SetActive(true);
    }

    void Update()
    {
        Debug.Log("Update UIController called");
        //Update health/hearts
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < (health / 20))
            {
                Debug.Log("Inside loop to enable heart");
                hearts[i].enabled = true;
            } else
            {
                Debug.Log("Inside loop to disable heart");
                hearts[i].enabled = false;
            }
        }

        //Check if won
        if (beatGame)
        {
            Invoke("BeatGame", startScreenDelay - 2f);
            if (Input.GetKeyDown("space"))
            {

                SceneManager.LoadScene("MainMenu");
            }
        }

        //Check if lost
        if (player.isDead)
        {
            Invoke("GameOver", startScreenDelay);
            if (Input.GetKeyDown("r"))
            {
                
                SceneManager.LoadScene(sceneName);
            }
            if (Input.GetKeyDown("space"))
            {

                SceneManager.LoadScene("MainMenu");
            }
        }

    }

    void GameOver()
    {
        gameOverImage.SetActive(true);
        playerObject.SetActive(false);
    }

    void BeatGame()
    {
        winImage.SetActive(true);
        playerObject.SetActive(false);
    }
}

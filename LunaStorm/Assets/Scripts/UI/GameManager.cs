using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance; //this will let us call methods from anywhere

    //We can add whichever field needs to be updated in text files
    // text scoreui
    // progress bar HP
    private GameObject myPlayer;
    private Camera cam;
    private CameraMovement camMov;
    private int score;
    private float playerHP;
    private bool lockedInBattle = false;

    // UI elements
    public GameObject inGameUI;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI gameOverText;

    [SerializeField] public GameObject[] lives;
    private int remainingLives;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        remainingLives = lives.Length;
        Debug.Log(" remaining lives: " + remainingLives);
        

        cam = FindAnyObjectByType<Camera>();
        if(cam.GetComponent<CameraMovement>())
        {
            camMov = cam.gameObject.GetComponent<CameraMovement>();
        }
        else
        {
            Debug.Log("you dont have the script");
        }
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(myPlayer);
        if(myPlayer.GetComponent<Health>())
        {
            //Debug.Log("Im hereeeee");
            myPlayer.GetComponent<Health>().enabled = true;
            playerHP = myPlayer.GetComponent<Health>().getHP();
        }


        instance = this;
        UpdateScore(0);
        UpdateHP(0);

        //lock cursor on start of game
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        if (gameOverUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        //Debug.Log(score);
        //update text object;
        scoreText.text = "Score: " + score;

    }

    public void UpdateHP(float amount)
    {
        playerHP += amount;
        //Debug.Log(playerHP);
    
    }
    public void EnterBattle()
    {
        camMov.enabled = false;
        lockedInBattle = true;
        Debug.Log("Battle Has Begun");
    }
    public void FinishBattle()
    {
        
        camMov.enabled = true;
        lockedInBattle = false;
        //Debug.Log("Wave finished you can go home now");
    }

    public bool LockedInBattle()
    {
        return lockedInBattle;
    }
   
    public void SubtractLives()
    {
        // subtract life
        remainingLives--;
        if(remainingLives<0)
        {
            remainingLives = 0;
        }
        // hide image of ship
        lives[remainingLives].SetActive(false);

        // if lives remaining is 0 call game over
        if (remainingLives == 0 && !isDead)
        {
            isDead = true;
            myPlayer.GetComponent<PlayerMovement>().enabled = false;
            myPlayer.GetComponent<PlayerAttack>().enabled = false;
            // display message on GameOver Screen
            gameOverText.text = "You Lost.  Try Again.";

           
            //call game over
            GameOverUI();
        }
    }

    public void GameOverUI()
    {
        myPlayer.SetActive(false);
        finalScoreText.text = "Final Score: " + score;
        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
    public void WinnerUI()
    {
        gameOverText.text = "You are victorious brave infiltrator";
        GameOverUI();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");  
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame");
    }
}

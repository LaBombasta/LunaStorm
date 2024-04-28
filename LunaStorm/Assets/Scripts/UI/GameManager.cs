using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
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
        if(myPlayer.GetComponent<Health>())
        {
            playerHP = myPlayer.GetComponent<Health>().getHP();
        }


        instance = this;
        UpdateScore(0);
        UpdateHP(0);
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
        Debug.Log("Wave finished you can go home now");
    }

    public bool LockecInBattle()
    {
        return lockedInBattle;
    }
}

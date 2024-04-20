using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //this will let us call methods from anywhere

    //We can add whichever field needs to be updated in text files
    // text scoreui
    // progress bar HP
    private Camera cam;
    private CameraMovement camMov;
    private int score;
    private float playerHP;

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
            Debug.Log("you dont have teh script");
        }
        
        
        instance = this;
        UpdateScore(0);
        UpdateHP(0);
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        Debug.Log(score);
        //update text object;

    }

    public void UpdateHP(float amount)
    {
        playerHP += amount;
        Debug.Log(playerHP);
    
    }
    public void EnterBattle()
    {
        camMov.enabled = false;
    }
    public void FinishWave()
    {
        camMov.enabled = true;
    }
}

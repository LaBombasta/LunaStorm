using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{

    [SerializeField] public GameObject[] lives;
    public int remainingLives;

    private bool isDead = false;

    public GameManager gameManager;

    public void Start()
    {
        remainingLives = lives.Length;
        Debug.Log (" remaining lives: " +  remainingLives);
    }

    public void SubtractLives()
    {
        // subtract life
        remainingLives--;
        
        // hide image of ship
        lives[remainingLives].SetActive(false);

        // if lives remaining is 0 call game over
        if (remainingLives == 0 && !isDead)
        {
            isDead = true;

            gameManager.gameOverText.text = "You Lost.  Try Again.";
            //call game over
            gameManager.GameOverUI();
        }
    }

    public void AddLives()
    {

    }

}

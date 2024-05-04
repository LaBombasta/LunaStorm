using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{

    [SerializeField] public GameObject[] lives;
    public int remainingLives;

    public GameManager gameManager;

    public void SubtractLives()
    {
        // subtract life
        remainingLives--;

        // hide image of ship
        lives[remainingLives].SetActive(false);

        // if lives remaining is 0 call game over
        if (remainingLives == 0)
        {
            //call game over
            gameManager.GameOverUI();
        }
    }

    public void AddLives()
    {

    }

}

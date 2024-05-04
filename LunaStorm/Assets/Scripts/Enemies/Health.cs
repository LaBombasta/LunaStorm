using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Renderer mySkin;

    [SerializeField] private int HP;
    [SerializeField] private int MaxHp;
    [SerializeField] private int hitScore;
    [SerializeField] private int deathScore;
    private Material blankCanvas;
    [SerializeField] private Color flash1 = Color.red;
    [SerializeField] private Color flash2 = Color.white;

    // need a gameobject for the scoring UI

   // point to lifecounter script
    public LifeCounter lifeCounter;

    

    private Material origColor;

    void Start()
    {
        //here is where we find the scoring UI
        //
        origColor = mySkin.material;
    }

    private void TakeDamage(int damage)
    {
        //Debug.Log("doing the thing");
        HP -= damage;
        if(HP >0)
        {
            StartCoroutine(Flash());
            
            if (gameObject.CompareTag("Player"))
            {
                GameManager.instance.UpdateHP(-damage);
                GameManager.instance.UpdateScore(hitScore);
                Debug.Log("Ouch");
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                //this is where you add points for hitting
                GameManager.instance.UpdateScore(hitScore);
                //Destroy(this.gameObject);
            }
            
        }
        else
        {
            if(gameObject.CompareTag("Player"))
            {
                Debug.Log("Am deadddd");
                // hide one of the ship images
                lifeCounter.SubtractLives();
                // when all lives are gone set player to inactive, pass score and game over reason to game over test boxes, and call Game over
                //start game over 
            }
            else if(gameObject.CompareTag("Enemy"))
            {
                GameManager.instance.UpdateScore(deathScore);
                gameObject.BroadcastMessage("ItemDrop", SendMessageOptions.DontRequireReceiver);
                Destroy(this.gameObject);
                //this is where you would instantiate a particle effect explosion.
            }
            
        }
    }

    public int getHP()
    {
        return HP;
    }

    
    IEnumerator Flash()
    {
        if(GetComponent<PlayerAttack>())
        {
            GetComponent<PlayerAttack>().enabled = false;
        }
        mySkin.material = blankCanvas;
        mySkin.material.color = flash1;
        yield return new WaitForSeconds(.05f);
        mySkin.material.color = flash2;
        yield return new WaitForSeconds(.05f);
        mySkin.material.color = flash1;
        yield return new WaitForSeconds(.05f);
        mySkin.material.color = flash2;
        yield return new WaitForSeconds(.05f);
        mySkin.material = origColor;

        yield return new WaitForSeconds(2.5f);
        if (GetComponent<PlayerAttack>())
        {
            GetComponent<PlayerAttack>().enabled =true;
        }
    }

}

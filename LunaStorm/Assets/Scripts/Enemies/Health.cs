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

    private Material origColor;
    [SerializeField]
    private bool noDamage;
    void Start()
    {
        //here is where we find the scoring UI
        //
        origColor = mySkin.material;
        noDamage = false;
    }

    private void TakeDamage(int damage)
    {
        //Debug.Log("doing the thing");
        if(noDamage)
        {
            Debug.Log("I took no damage on this instance");
        }
        else
        {
            HP -= damage;
            if (HP > 0)
            {
                StartCoroutine(Flash());

                if (gameObject.CompareTag("Player"))
                {
                    GameManager.instance.UpdateHP(-damage);
                    GameManager.instance.UpdateScore(hitScore);
                    GameManager.instance.SubtractLives();
                    //Debug.Log("Ouch");
                }
                else if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Turret"))
                {
                    //Debug.Log(HP);
                    //this is where you add points for hitting
                    GameManager.instance.UpdateScore(hitScore);
                    if (gameObject.GetComponent<Boss>())
                    {
                        gameObject.GetComponent<Boss>().CalculatePhase();
                    }

                    //Destroy(this.gameObject);
                }

            }
            else
            {
                if (gameObject.CompareTag("Player"))
                {
                    //Debug.Log("Am deadddd");
                    // hide one of the ship images
                    GameManager.instance.SubtractLives();
                    // when all lives are gone set player to inactive, pass score and game over reason to game over test boxes, and call Game over
                    //start game over 
                }
                else if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Turret"))
                {
                    Debug.Log("HI there");
                    GameManager.instance.UpdateScore(deathScore);
                    Destroy(this.gameObject);
                    //this is where you would instantiate a particle effect explosion.
                }

            }
        }

       
    }
    public void NoDamage(float num)
    {
        StartCoroutine(INVINCIBLE(num));
    }

    private IEnumerator INVINCIBLE(float timer)
    {
        //Debug.Log(this.gameObject);
        noDamage = true;
        yield return new WaitForSeconds(timer);
        noDamage = false;
    }
    public int getHP()
    {
        return HP;
    }
    public int getMaxHP()
    {
        return MaxHp;
    }

    
    IEnumerator Flash()
    {
        //Debug.Log("Starting");
        if(GetComponent<PlayerAttack>())
        {
            GetComponent<PlayerAttack>().enabled = false;
        }
        mySkin.material = blankCanvas;
        float timer = 0;
        while (timer < 1.5f)
        {
            mySkin.material.color = flash1;
            yield return new WaitForSeconds(.05f);
            mySkin.material.color = flash2;
            yield return new WaitForSeconds(.05f);
            mySkin.material.color = flash1;
            yield return new WaitForSeconds(.05f);
            mySkin.material.color = flash2;
            yield return new WaitForSeconds(.05f);
            timer += Time.deltaTime +.2f;
            yield return null;
        }
        mySkin.material = origColor;

        if (GetComponent<PlayerAttack>())
        {
            GetComponent<PlayerAttack>().enabled =true;
        }
    }

}

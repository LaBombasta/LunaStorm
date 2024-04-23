using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Renderer mySkin;

    [SerializeField] private float HP;
    [SerializeField] private float MaxHp;
    [SerializeField] private int hitScore;
    [SerializeField] private int deathScore;
    [SerializeField] private Material blankCanvas;
    [SerializeField] private Color flash1 = Color.red;
    [SerializeField] private Color flash2 = Color.white;

    // need a gameobject for the scoring UI
    

    private Material origColor;

    void Start()
    {
        //here is where we find the scoring UI
        //
        origColor = mySkin.material;
        TakeDamage(-1);
    }

    private void TakeDamage(float damage)
    {
        //Debug.Log("doing the thing");
        HP -= damage;
        if(HP >0)
        {
            StartCoroutine(Flash());
            
            if (gameObject.CompareTag("Player"))
            {
                GameManager.instance.UpdateHP(-damage);
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
                //kill player
            }
            else if(gameObject.CompareTag("Enemy"))
            {
                GameManager.instance.UpdateScore(deathScore);
                Destroy(this.gameObject);
                //this is where you would instantiate a particle effect explosion.
            }
            
        }
    }
    
    IEnumerator Flash()
    {
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
    }

}

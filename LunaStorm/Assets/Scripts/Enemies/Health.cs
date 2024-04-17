using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float HP;
    public Renderer mySkin;
    private string myTag;

    void Start()
    {
        myTag = gameObject.tag;
        //here is where we find the scoring UI
    }

    private void TakeDamage(float damage)
    {
        HP -= damage;
    }
    
    IEnumerator Flash()
    {
        yield return new WaitForSeconds(.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("I've been created");
        Destroy(this.gameObject, 10.0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.AddLife();
            Destroy(gameObject);
        }
    }
}

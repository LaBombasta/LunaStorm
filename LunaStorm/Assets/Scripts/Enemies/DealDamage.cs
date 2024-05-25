using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damage;
    public string tagToHit;
    public GameObject explosionPrefab; // Variable to hold the explosion prefab
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject);
        //Debug.Log("chirp");
        if(collision.gameObject.CompareTag(tagToHit))
        {
            collision.transform.gameObject.BroadcastMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            GameObject temp  = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(temp, 1);

            Destroy(this.gameObject);
        }
    }
}


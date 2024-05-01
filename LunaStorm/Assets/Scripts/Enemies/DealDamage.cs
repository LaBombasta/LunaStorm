using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damage;
    public string tagToHit;
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject);
        //Debug.Log("chirp");
        if(collision.gameObject.CompareTag(tagToHit))
        {
            collision.transform.gameObject.BroadcastMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
    }
}


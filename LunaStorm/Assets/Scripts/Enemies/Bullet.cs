using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     //Note: This bullet will only work for air bound enemies. It has no Y movement. 

    public float bulletLifeTime = 1f;
    public float speed = 1f;
    public float rotation = 0f;
    private Vector3 spawnPoint;
    private float timer = 0f;

    void Start()
    {
        spawnPoint = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLifeTime) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }


    private Vector3 Movement(float timer)
    {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float z = timer * speed * transform.right.z;

        Vector3 test = new Vector3(x + spawnPoint.x, spawnPoint.y, z + spawnPoint.z);// spawnPoint.z);
      
        //Debug.Log(test);
        return test;
    }

    private void OnCollisionEnter(Collision collision)
    {

        collision.transform.gameObject.BroadcastMessage("TakeDamage", 1f, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }

    void setSetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}

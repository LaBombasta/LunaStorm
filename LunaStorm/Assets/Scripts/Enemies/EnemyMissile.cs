using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    private GameObject target;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float missileSpeed;
    [SerializeField]
    private float lockDistance;
    private bool lockedOn = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += missileSpeed * Time.deltaTime * transform.forward;
        
        
        //direction = direction.normalized;
        if(target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            float leash = Mathf.Abs(direction.magnitude);
            if (leash <= lockDistance && !lockedOn)
            {
                lockedOn = true;
                StartCoroutine(LookAt());
            }
        }
       
        
    }
    private IEnumerator LookAt()
    {
        
        float time = 0;
        while(time<1)
        {
            if(target == null)
            {
                break;
            }
            Quaternion look = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, time);
            time += Time.deltaTime * turnSpeed;
            yield return null;
        }
    }
}

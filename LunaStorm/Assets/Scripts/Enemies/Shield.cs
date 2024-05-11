using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private Vector3 newScale;
    [SerializeField]
    private float timer;
    [SerializeField]
    private Vector3 shrinkScale;
    [SerializeField]
    private float shrinkTimer;
    [SerializeField]
    private int HP;
    [SerializeField]
    private Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
        StartCoroutine(GrowOverTime());
    }

    IEnumerator GrowOverTime()
    {
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.localScale += newScale * Time.deltaTime;
            yield return null;
        }
    }


    private void TakeDamage(int damage)
    {
        Debug.Log("hi");
        HP -= damage;
       if(HP<=0)
        {
            StartCoroutine(ShrinkOverTime());
        }
    }


    IEnumerator ShrinkOverTime()
    {
        while (shrinkTimer >= 0)
        {
            shrinkTimer -= Time.deltaTime;
            transform.localScale -= newScale * Time.deltaTime;
            if(transform.localScale.x>=1)
            {
                StopAllCoroutines();
                Destroy(this.gameObject);
            }
            yield return null;
        }
        boss.SetMoveType(0);

        Destroy(this.gameObject);
    }

}

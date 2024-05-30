using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Transform transform = FindAnyObjectByType<Camera>().transform;
        this.gameObject.transform.SetParent(transform);
    }

}

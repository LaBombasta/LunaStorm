using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LevelChange : MonoBehaviour
{
    private EndLevel1IGC endLevel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) { Destroy(gameObject); }
    }

    // This method is called when the object is destroyed
    private void OnDestroy()
    {
        // Check if the destroyed object has the tag "FinalBoss"
        if (gameObject.CompareTag("FinalBoss"))
        {
            endLevel.IGC();
        }
    }
}

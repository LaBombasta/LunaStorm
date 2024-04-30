using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LevelChange : MonoBehaviour
{
    private EndLevel1IGC endLevel;

    GameObject finalBoss;

    private void Start()
    {
        // Assuming EndLevel1IGC is the script attached to an object in your scene.
        endLevel = FindObjectOfType<EndLevel1IGC>();

        // Use FindGameObjectWithTag to find an object with a specific tag.
        finalBoss = GameObject.FindGameObjectWithTag("FinalBoss");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Check if the finalBoss object is found before destroying it.
            if (finalBoss != null)
            {
                Destroy(finalBoss);
            }
        }

        // Check if the finalBoss object has been destroyed
        // You can simply check if finalBoss is null to determine if it's been destroyed.
        if (finalBoss == null)
        {
            // Call the IGC method from EndLevel1IGC script
            if (endLevel != null)
            {
                endLevel.IGC();
            }
            else
            {
                Debug.LogWarning("EndLevel1IGC script reference not set.");
            }
        }
    }
}

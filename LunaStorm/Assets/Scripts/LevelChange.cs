using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LevelChange : MonoBehaviour
{
    public GameObject finalBoss; // Public variable to hold the final boss GameObject

    private EndLevel1IGC endLevel;
    private bool hasTriggeredIGC = false; // Flag to track if IGC sequence has been triggered

    private void Start()
    {
        endLevel = FindObjectOfType<EndLevel1IGC>();

        // Check if the final boss GameObject is not assigned
        if (finalBoss == null)
        {
            Debug.LogWarning("Final Boss GameObject not assigned.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Check if the final boss GameObject is assigned and exists
            if (finalBoss != null && finalBoss.activeSelf)
            {
                Destroy(finalBoss);
            }
        }

        // Check if the final boss GameObject is destroyed, endLevel is not null, and IGC has not been triggered
        if (finalBoss == null && endLevel != null && !hasTriggeredIGC)
        {
            endLevel.IGC();
            hasTriggeredIGC = true; // Set the flag to true to prevent further triggering
        }
    }
}

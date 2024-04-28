using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel1IGC : MonoBehaviour
{
    public string moveableTag = "moveable"; // Tag of the objects you want to find

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IGC()
    {
        // Find all objects with the tag "moveable" and store them in an array
        GameObject[] moveableObjects = GameObject.FindGameObjectsWithTag(moveableTag);

        // Loop through each found object
        foreach (GameObject obj in moveableObjects)
        {
            // Check if the object's name contains "Gate_Left"
            if (obj.name.Contains("Gate_Left"))
            {
                // Lerp the object's position
                MoveObject(obj.transform, new Vector3(20f, 0f, 0f)); // Move +20 in the x-direction
            }
            // Check if the object's name contains "Gate_Right"
            else if (obj.name.Contains("Gate_Right"))
            {
                // Lerp the object's position
                MoveObject(obj.transform, new Vector3(-20f, 0f, 0f)); // Move -20 in the x-direction
            }
        }
    }

    // Coroutine to lerp object position
    IEnumerator LerpObjectPosition(Transform objectTransform, Vector3 targetPosition, float lerpSpeed)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = objectTransform.position;

        while (elapsedTime < lerpSpeed)
        {
            // Calculate the interpolation value
            float t = elapsedTime / lerpSpeed;

            // Lerp the position
            objectTransform.position = Vector3.Lerp(startingPosition, targetPosition, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the object reaches the target position
        objectTransform.position = targetPosition;
    }

    // Move the object to the target position
    private void MoveObject(Transform objectTransform, Vector3 targetPosition)
    {
        float lerpSpeed = 0.5f; // Adjust as needed
        StartCoroutine(LerpObjectPosition(objectTransform, objectTransform.position + targetPosition, lerpSpeed));
    }
}

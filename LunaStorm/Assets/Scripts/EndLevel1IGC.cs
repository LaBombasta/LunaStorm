using System.Collections;
using UnityEngine;

public class EndLevel1IGC : MonoBehaviour
{
    private string moveableTag = "Moveable"; // Tag of the objects you want to find

    GameObject player;

    Camera mainCamera;

    PlayerMovement playMov;
    CameraMovement camMov;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;

        playMov = FindObjectOfType<PlayerMovement>();
        camMov = FindObjectOfType<CameraMovement>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IGC()
    {
        playMov.enabled = false;
        camMov.enabled = false;
        gameManager.enabled = false;

        StartCoroutine(MoveCamera());

        // Find all objects with the tag "moveable" and store them in an array
        GameObject[] moveableObjects = GameObject.FindGameObjectsWithTag(moveableTag);

        // Loop through each found object
        foreach (GameObject obj in moveableObjects)
        {
            // Check if the object's name contains "Gate_Left"
            if (obj.name.Contains("Gate_Left"))
            {
                // Lerp the object's position
                MoveObject(obj.transform, new Vector3(-20f, 0f, 0f)); // Move -20 in the x-direction
            }
            // Check if the object's name contains "Gate_Right"
            else if (obj.name.Contains("Gate_Right"))
            {
                // Lerp the object's position
                MoveObject(obj.transform, new Vector3(20f, 0f, 0f)); // Move +20 in the x-direction
            }
        }

        StartCoroutine(MovePlayer());

    }

    //Coroutine to move camera behind player
    IEnumerator MoveCamera()
    {
        Vector3 targetCameraPosition = player.transform.position - player.transform.forward * 20.0f;
        while (Vector3.Distance(mainCamera.transform.position, targetCameraPosition) > 0.05f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCameraPosition, Time.deltaTime * 5f);
            yield return null;
        }
    }

    IEnumerator MovePlayer()
    {
        Vector3 targetPlayerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1.0f);
        while (Vector3.Distance(player.transform.position, targetPlayerPosition) > 0.05f)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, targetPlayerPosition, Time.deltaTime * 5f);
            yield return null;
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
        float lerpSpeed = 1.5f; // Adjust as needed
        StartCoroutine(LerpObjectPosition(objectTransform, objectTransform.position + targetPosition, lerpSpeed));
    }
}

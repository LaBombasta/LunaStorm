using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel1IGC : MonoBehaviour
{
    private string moveableTag = "Moveable"; // Tag of the objects you want to find

    GameObject player;

    Camera mainCamera;

    private bool isCameraMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IGC()
    {
        StartCoroutine(IGCSequence());
        player.layer = LayerMask.NameToLayer("NoCollision");
    }

    void DisableScriptsExcept(params string[] allowedScripts)
    {
        // Get all GameObjects in the scene
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        // List to store the allowed scripts
        List<MonoBehaviour> allowedScriptInstances = new List<MonoBehaviour>();

        // Add allowed scripts to the list
        foreach (string scriptName in allowedScripts)
        {
            MonoBehaviour[] scriptInstances = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptInstances)
            {
                if (script.GetType().Name.Equals(scriptName))
                {
                    allowedScriptInstances.Add(script);
                }
            }
        }

        // Disable all scripts except the allowed ones
        foreach (GameObject obj in allObjects)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (!allowedScriptInstances.Contains(script))
                {
                    script.enabled = false;
                }
            }
        }
    }

    // Move the object to the target position
    IEnumerator MoveObject(Transform objectTransform, Vector3 targetPosition, float lerpSpeed)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = objectTransform.position;

        while (elapsedTime < lerpSpeed && isCameraMoving)
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

    //Coroutine to move camera behind player
    IEnumerator MoveCamera()
    {
        // Define the target position behind the player
        Vector3 targetPosition = player.transform.position - player.transform.forward * 20f; // Move 20 units behind the player
        targetPosition.y = player.transform.position.y + 10.0f; // Adjust as needed for height

        // Define the speed of camera movement
        float lerpSpeed = 10.0f; // Adjust as needed

        // Get the starting position of the camera
        Vector3 startingPosition = mainCamera.transform.position;

        // Calculate the distance between the starting and target positions
        float distance = Vector3.Distance(startingPosition, targetPosition);

        // Calculate the time it takes to reach the target position based on the distance and lerp speed
        float lerpTime = distance / lerpSpeed;

        // Initialize elapsed time
        float elapsedTime = 0f;

        // Loop until the camera reaches the target position
        while (elapsedTime < lerpTime)
        {
            // Calculate the interpolation value
            float t = elapsedTime / lerpTime;

            // Lerp the position of the camera
            mainCamera.transform.position = Vector3.Lerp(startingPosition, targetPosition, t);

            // Check if the camera's Z position is less than the player's Z position
            if (mainCamera.transform.position.z < player.transform.position.z)
            {
                // Make the camera look at the player with an up direction to avoid flipping
                mainCamera.transform.LookAt(player.transform.position + Vector3.up * 2.0f); // Adjust the offset as needed
            }

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the camera reaches the target position
        mainCamera.transform.position = targetPosition;

        // Check if the camera's Z position is less than the player's Z position
        if (mainCamera.transform.position.z < player.transform.position.z)
        {
            // Make sure the camera is looking at the player with an up direction
            mainCamera.transform.LookAt(player.transform.position + Vector3.up * 2.0f); // Adjust the offset as needed
        }
    }

    //Coroutine to move player
    IEnumerator MovePlayer()
    {
        float moveSpeed = 100.075f; // Adjust this speed as needed

        while (true) // Infinite loop
        {
            // Calculate the movement direction (forward)
            Vector3 moveDirection = Vector3.forward;

            // Move the player using CharacterController
            player.GetComponent<CharacterController>().Move(moveDirection * moveSpeed * Time.deltaTime);

            //Debug.Log("loooooooooooooooooooop");

            // Check for collisions with objects tagged "Finish"
            Collider[] colliders = Physics.OverlapSphere(player.transform.position, 1.0f); // Adjust radius as needed
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Finish"))
                {

                    // Load the next scene (Level 2)
                    SceneManager.LoadScene("Victory");

                    yield break; // Exit the coroutine
                }
            }

            yield return null;
        }

    }


    IEnumerator IGCSequence()
    {
        // Disable Scripts
        DisableScriptsExcept("LevelChange", "EndLevel1IGC");

        // Lerp the player's position to x transform = 0
        yield return StartCoroutine(MoveObject(player.transform, new Vector3(0.0f, player.transform.position.y, player.transform.position.z), 3.0f));

        // Move Camera
        yield return StartCoroutine(MoveCamera());

        // Move Objects
        GameObject[] moveableObjects = GameObject.FindGameObjectsWithTag(moveableTag);
        foreach (GameObject obj in moveableObjects)
        {
            if (obj.name.Contains("Gate_Left"))
            {
                Vector3 targetPosition = obj.transform.position + new Vector3(-20f, 0f, 0f); // Move -20 in the x-direction
                StartCoroutine(MoveObject(obj.transform, targetPosition, 1.5f));
                Destroy(obj, 2.0f);
            }
            else if (obj.name.Contains("Gate_Right"))
            {
                Vector3 targetPosition = obj.transform.position + new Vector3(20f, 0f, 0f); // Move +20 in the x-direction
                StartCoroutine(MoveObject(obj.transform, targetPosition, 1.5f));
                Destroy(obj, 2.0f);
            }
        }

        // Move Player
        yield return StartCoroutine(MovePlayer());
    }
}

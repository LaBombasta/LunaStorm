using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Camera Follow Target
    [SerializeField]
    protected Transform target = null;

    //Width and height of the follow rectagle
    [SerializeField]
    protected float rectWidth = 200;
    [SerializeField]
    protected float rectHeight = 100;

    //Camera move speed
    [SerializeField]
    protected float cameraMoveSpeed = 1.25f;

    //The follow rectangle
    protected Rect followRect = Rect.zero;

    //Show follow rectangle debug
    [SerializeField]
    protected bool debugFollowRect = true;


    // Start is called before the first frame update
    void Start()
    {
        //Initialize the follow rectangle
        followRect = new Rect(((Screen.width/2) - (rectWidth/2)), ((Screen.height/2)-(rectHeight/2)), rectWidth, rectHeight);
    }

    private void OnGUI()
    {
        if(debugFollowRect)
        {
            //Draw the followRect
            GUI.Box(followRect, "");
        }
    }

    //Return the world position (Vector3) of a screen space Vector2
    Vector3 GetWorldPoint(float x, float y)
    {


        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, Vector3.Distance(transform.position, target.position)));
    }

    // Update is called once per frame
    void Update()
    {
        //Distance between camera and target along x
        //As we get closer the camera will slow down
        //As we get further the camera will speed up
        float xDistance = target.position.x - transform.position.x;

        //Distance at which the camera position will change
        float cameraStep = (cameraMoveSpeed * Time.deltaTime * xDistance) / 2;

        //Right side of the follow rectangle
        float rightRect = GetWorldPoint(followRect.xMax, followRect.center.y).x;

        //If the target is to the right of the follow rectangle
        if (target.position.x > rightRect)
        {
            //Move Right
            transform.position += transform.right * cameraStep;
        }

        //Draw debug ray
        Debug.DrawRay(GetWorldPoint(followRect.xMax, followRect.center.y), Vector3.forward, Color.red);


        //Left side of the follow rectangle
        float leftRect = GetWorldPoint(followRect.xMin, followRect.center.y).x;

        //If the target is to the left of the follow rectangle
        if (target.position.x < leftRect)
        {
            //Move left
            transform.position += transform.right * cameraStep;
        }

        //Draw debug ray
        Debug.DrawRay(GetWorldPoint(followRect.xMin, followRect.center.y), Vector3.forward, Color.red);

        //Top of the follow rectangle
        float topRect = GetWorldPoint(followRect.center.x, followRect.yMax).y;

        //If the target is above the follow rectangle
        if (target.position.y > topRect)
        {
            //Move up
            transform.position += transform.up * Time.deltaTime;
        }

        //Draw debug ray
        Debug.DrawRay(GetWorldPoint(followRect.center.x, followRect.yMax), Vector3.forward, Color.red);

        //Bottom of the follow rectangle
        float bottomRect = GetWorldPoint(followRect.center.x, followRect.yMin).y;

        //If the target is below the follow rectangle
        if (target.position.y < bottomRect)
        {
            //Move down
            transform.position -= transform.up * Time.deltaTime;
        }

        //Draw debug ray
        Debug.DrawRay(GetWorldPoint(followRect.center.x, followRect.yMin), Vector3.forward, Color.red);
    }
}

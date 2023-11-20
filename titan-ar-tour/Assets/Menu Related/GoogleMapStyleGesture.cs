using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class GoogleMapStyleGesture : MonoBehaviour
{
    public GameObject map; //map must have its center at (0,0,0) local position
    public float minSize = 400; //min max size of this orthogonal camer
    public float maxSize = 900;
    public float touchZoomSpeed = 0.01f; //multiplier for touch zoom to otho camera size

    public float inertiaDampingVal = 1.03f; //dividing factor for how quick inertia damp off

    private Camera cam;

    //drag vars
    private Vector3 dragStart;
    private float dragSSx, dragSSy;
    private bool wasDown = false;
    private bool nowDown;
    private Vector3 mapCenter;
    private Vector3 originalWS;

    private float mapHalfWidth, mapHalfHeight, camHalfWidth, camHalfHeight;

    private float minX, maxX, minZ, maxZ, minY, maxY;
    private Vector3 newLocalPos;

    private Vector3 inertiaVector;

    public GameObject[] objectsToMonitor;
    private bool disableInteractions = false;

    /*
    void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 150, 100),
            "minX" + minX + "\n" +
            "maxX" + maxX + "\n" +
            "minZ" + minZ + "\n" +
            "maxZ" + maxZ + "\n" +
            "newLocalPos" + newLocalPos.ToString()
        );

    }
    */

    // Use this for initialization
    private Image mapImage;

    void Start()
    {
        inertiaVector = new Vector3(0, 0, 0);
        cam = GetComponent<Camera>();
        mapCenter = map.transform.localPosition;
        newLocalPos = Vector3.zero;

        mapImage = map.GetComponent<Image>();
        if (mapImage != null)
        {
            Rect mapRect = mapImage.rectTransform.rect;
            mapHalfWidth = mapRect.width * 0.5f * map.transform.localScale.x;
            mapHalfHeight = mapRect.height * 0.5f * map.transform.localScale.y;
        }
        else
        {
            Debug.LogError("Image component missing on the map object!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        CheckObjectsStatus();
        if (!disableInteractions)
        {
            processDrag();
            processZoom();
        }
    }

    private void CheckObjectsStatus()
    {
        disableInteractions = false;
        foreach (GameObject obj in objectsToMonitor)
        {
            if (obj.activeInHierarchy)
            {
                disableInteractions = true;
                break;
            }
            if (GameObject.Find("Dropdown List"))
            {
                disableInteractions = true;
                break;
            }
        }
    }

    private void processDrag()
    {
        //damp inertia
        inertiaVector = inertiaVector / inertiaDampingVal;
        if (inertiaVector.magnitude < 0.005)
        {
            inertiaVector = new Vector3(0, 0, 0);
        }

        //check if input is activated (mouse down or finger touching screen)
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            nowDown = (Input.touchCount > 0);
        }
        else
        {
            nowDown = (Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0);
        }

        //calcualte how much camera need to change position
        Vector3 WSDiff = Vector3.zero;

        //if mouse currently holding or finger touching the screen
        if (nowDown)
        {
            //get screenspace input points
            //phone
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount == 1)
                {
                    //one finger
                    dragSSx = Input.GetTouch(0).position.x;
                    dragSSy = Input.GetTouch(0).position.y;
                }
                else if (Input.touchCount == 2)
                {
                    //check if player is switching between 1 finger to 2 finger
                    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Ended)
                    {
                        //if player add or remove finger, recalculate origional World Space point so that the map wouldn't jump around
                        wasDown = false;
                        return;
                    }
                    //two finger
                    dragSSx = (Input.GetTouch(0).position.x + Input.GetTouch(1).position.x) / 2;
                    dragSSy = (Input.GetTouch(0).position.y + Input.GetTouch(1).position.y) / 2;
                }
            }
            else
            {
                dragSSx = Input.mousePosition.x;
                dragSSy = Input.mousePosition.y;
            }

            //calcualte how much camera need to change position
            if (!wasDown)
            {
                originalWS = cam.ScreenToWorldPoint(new Vector3(dragSSx, dragSSy, 1));
            }
            else if (wasDown)
            {
                Vector3 newWS = cam.ScreenToWorldPoint(new Vector3(dragSSx, dragSSy, 1));
                WSDiff = (originalWS - newWS);
                inertiaVector = WSDiff;

            }
        }
        wasDown = nowDown;

        //move camera by WS diff vector or inertiaVector
        if (nowDown)
        {
            newLocalPos = transform.localPosition + WSDiff;
        }
        else
        {
            newLocalPos = transform.localPosition + inertiaVector;
        }
       
        camHalfHeight = 2f * cam.orthographicSize / 2;
        camHalfWidth = camHalfHeight * cam.aspect;
        minX = mapCenter.x - mapHalfWidth + camHalfWidth; //min position camera should be at
        maxX = mapCenter.x + mapHalfWidth - camHalfWidth;
        minZ = mapCenter.z - mapHalfHeight + camHalfHeight;
        maxZ = mapCenter.z + mapHalfHeight - camHalfHeight;
        minY = mapCenter.y - mapHalfHeight + camHalfHeight;
        maxY = mapCenter.y + mapHalfHeight - camHalfHeight;

        if (minX > maxX)
        { //incase the scale is too small, put camera at center
            newLocalPos = new Vector3(mapCenter.x, newLocalPos.y, newLocalPos.z);
        }
        else
        {
            if (newLocalPos.x - mapCenter.x < minX)
            {
                // Debug.Log("MIN X");
                newLocalPos = new Vector3(minX, newLocalPos.y, newLocalPos.z);
            }
            if (newLocalPos.x - mapCenter.x > maxX)
            {
                // Debug.Log("MIN Z");
                newLocalPos = new Vector3(maxX, newLocalPos.y, newLocalPos.z);
            }
        }
        if (minY > maxY)
        { //incase the scale is too small, put camera at center
            newLocalPos = new Vector3(newLocalPos.x, mapCenter.y, newLocalPos.z);
        }
        else
        {
            if (newLocalPos.y - mapCenter.y < minY)
            {
                // Debug.Log("MIN Y");
                newLocalPos = new Vector3(newLocalPos.x, minY, newLocalPos.z);
            }
            if (newLocalPos.y - mapCenter.y > maxY)
            {
                // Debug.Log("MAX Y");
                newLocalPos = new Vector3(newLocalPos.x, maxY, newLocalPos.z);
            }
        }
        if (minZ > maxZ)
        { //incase the scale is too small, put camera at center
            newLocalPos = new Vector3(newLocalPos.x, newLocalPos.y, mapCenter.z);
        }
        else
        {
            if (newLocalPos.z - mapCenter.z < minZ)
            {
                // Debug.Log("MIN Z");
                newLocalPos = new Vector3(newLocalPos.x, newLocalPos.y, minZ);
            }
            if (newLocalPos.z - mapCenter.z > maxZ)
            {
                // Debug.Log("MAX Z");
                newLocalPos = new Vector3(newLocalPos.x, newLocalPos.y, maxZ);
            }
        }
       // newLocalPos.y = Mathf.Clamp(newLocalPos.y, y_lower_limit, y_upper_limit) ; // limit camera height on y axis
        transform.localPosition = newLocalPos;
    }

    private void processZoom()
    {
        //Get increment values
        float increment = 0;
        //touch screen
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount == 2)
            {
                //two finger
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                //scale the zoom increment value base on touchZoomSpeed
                increment = deltaMagnitudeDiff * touchZoomSpeed;
            }
        }
        else
        {
            //mice and keyboard
            float zoomSpeedMultiplier = 30.0f;
            increment = Input.GetAxis("Mouse ScrollWheel") * zoomSpeedMultiplier * -1;
        }
        //calculate new orthographic camera size
        float currentSize = cam.orthographicSize;
        currentSize += increment;

        //check if size is out of the defined bound
        if (currentSize >= maxSize)
        {
            currentSize = maxSize;
        }
        else if (currentSize <= minSize)
        {
            currentSize = minSize;
        }

        //set new size
        cam.orthographicSize = currentSize;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.XR.ARCoreExtensions;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
// using UnityEngine.Ink;
namespace AR_Fukuoka
{
    public class SampleScript : MonoBehaviour
    {
        // Ex. myLogger.Log(kTag, "Log String");
        private Logger myLogger;
        private static string kTag = "myLogger";


        [SerializeField] ARRaycastManager m_RaycastManager;
        List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();


        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON_default;
        [SerializeField] private TextAsset inkJSON_ecs;
        [SerializeField] private TextAsset inkJSON_tsu;
        [SerializeField] private TextAsset inkJSON_titanShops;
        [SerializeField] private TextAsset inkJSON_healthCenter;
        [SerializeField] private TextAsset inkJSON_pollakLibrary;
        [SerializeField] private TextAsset inkJSON_humanities;
        [SerializeField] private TextAsset inkJSON_mihaylo;
        [SerializeField] private TextAsset inkJSON_mccarthy;
        [SerializeField] private TextAsset inkJSON_visualArts;
        [SerializeField] private TextAsset inkJSON_recCenter;
        [SerializeField] private TextAsset inkJSON_khs;





        Camera arCam;
        //GeospatialAPI
        public AREarthManager EarthManager;
        //GeospatialAPI ARCore
        public VpsInitializer Initializer;


        //UI 
        public Text OutputText;

        // public double HeadingThreshold = 25; //Allowable accuracy of azimuth [ORIGINAL]
        // public double HorizontalThreshold = 20; //Allowable accuracy of horizontal position [ORIGINAL]

        public double HeadingThreshold = 15; //Allowable accuracy of azimuth [ORIGINAL]
        public double HorizontalThreshold = 10; //Allowable accuracy of horizontal position [ORIGINAL]

        private struct prefabLocation //Struct to hold the prefab coordinates
        {
            public double Latitude;
            public double Longitude;

            public prefabLocation(double Latitude, double Longitude) // Constructor for our struct
            {
                this.Latitude = Latitude;
                this.Longitude = Longitude;
            }
        }

        // Predefined Prefab locations

        // Placentia Location for test
        private prefabLocation myHomePrefab = new prefabLocation(33.87295, -117.86195);

        // private prefabLocation myHomePrefab = new prefabLocation(33.9843667988129, -117.899593302068);
        private prefabLocation recCenterPrefab = new prefabLocation(33.88271273750798, -117.88765951159485);
        private prefabLocation titanShopsPrefab = new prefabLocation(33.88154974343308, -117.88678588803086);
        private prefabLocation tsuPrefab = new prefabLocation(33.881619769735295, -117.88743881625606);
        private prefabLocation pollakLibraryPrefab = new prefabLocation(33.88152899488228, -117.88577994118933);
        private prefabLocation healthCenterPrefab = new prefabLocation(33.88272091885041, -117.88424348643913);
        private prefabLocation ecsPrefab = new prefabLocation(33.88232971232807, -117.88295649608438);
        private prefabLocation humanitiesPrefab = new prefabLocation(33.88045607974854, -117.88448836035454);
        private prefabLocation khsPrefab = new prefabLocation(33.882568430755555, -117.88574861102796);
        private prefabLocation visualArtsPrefab = new prefabLocation(33.88073749292297, -117.88903833498848);
        private prefabLocation mihayloPrefab = new prefabLocation(33.87886735588893, -117.88348377765463);
        private prefabLocation mccarthyPrefab = new prefabLocation(33.879974697980664, -117.88556579983258);

        private GameObject myHomeObj, recCenterObj, titanShopsObj, tsuObj, pollakLibraryObj, healthCenterObj, ecsObj,
        humanitiesObj, khsObj, visualArtsObj, mihayloObj, mccarthyObj;

        private GameObject currentPrefab;

        // This bool is used to instantiate our objects only once.
        public double Altitude; //Height to place the object
        public double Heading; //Object Orientation (N=0)
        public GameObject ContentPrefab; //Original Data of display object
        public ARAnchorManager AnchorManager; //Used to create anchors

        // Start is called before the first frame update
        void Start()
        {
            //Initialize logger
            myLogger = new Logger(Debug.unityLogger.logHandler);

            // First, instantiate our GameObjects to null. This will be our prefabs
            myHomeObj = null;
            recCenterObj = null;
            titanShopsObj = null;
            tsuObj = null;
            pollakLibraryObj = null;
            healthCenterObj = null;
            ecsObj = null;
            humanitiesObj = null;
            khsObj = null;
            visualArtsObj = null;
            mihayloObj = null;
            mccarthyObj = null;
            currentPrefab = null;
            arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }
        // Update is called once per frame
        void Update()
        {
            string status = "";
            // If initialization fails or you do not want to track, do nothing and return
            if (!Initializer.IsReady || EarthManager.EarthTrackingState != TrackingState.Tracking)
            {
                return;
            }
            // Get tracking results
            GeospatialPose pose = EarthManager.CameraGeospatialPose;

            // Describe here the behavior according to tracking accuracy
            //Tracking accuracy is worse than the schedule
            if (pose.HeadingAccuracy > HeadingThreshold ||
                 pose.HorizontalAccuracy > HorizontalThreshold)
            {
                status = "Low tracking accuracy";
                // myLogger.Log(kTag, "LOW TRACKING ACCURACY");
            }
            else //Tracking accuracy is above threshold 
            {
                // Call our instantiate function to instantiate our prefabs
                status = "High tracking accuracy";
                // myLogger.Log(kTag, "HIGH TRACKING ACCURACY");
                instantiatePrefab(ref myHomeObj, myHomePrefab, pose, "myHomeObj");
                instantiatePrefab(ref recCenterObj, recCenterPrefab, pose, "recCenterObj");
                instantiatePrefab(ref titanShopsObj, titanShopsPrefab, pose, "titanShopsObj");
                instantiatePrefab(ref tsuObj, tsuPrefab, pose, "tsuObj");
                instantiatePrefab(ref pollakLibraryObj, pollakLibraryPrefab, pose, "pollakLibraryObj");
                instantiatePrefab(ref healthCenterObj, healthCenterPrefab, pose, "healthCenterObj");
                instantiatePrefab(ref ecsObj, ecsPrefab, pose, "ecsObj");
                instantiatePrefab(ref humanitiesObj, humanitiesPrefab, pose, "humanitiesObj");
                instantiatePrefab(ref khsObj, khsPrefab, pose, "khsObj");
                instantiatePrefab(ref visualArtsObj, visualArtsPrefab, pose, "visualArtsObj");
                instantiatePrefab(ref mihayloObj, mihayloPrefab, pose, "mihayloObj");
                instantiatePrefab(ref mccarthyObj, mccarthyPrefab, pose, "mccarthyObj");

                // myLogger.Log(kTag, "DONE INSTANTIATING");
            }

            if (Input.touchCount == 0)
            {
                ShowTrackingInfo(status, pose, 0, "");
                return;
            }

            if (!DialogueManager.GetInstance().dialogueIsPlaying)
            {
                // myLogger.Log(kTag, "Dialogue is NOT playing");
                if (currentPrefab != null)
                {
                    currentPrefab.GetComponent<Outline>().enabled = false;
                }
                currentPrefab = null;
            }
            else
            {
                // myLogger.Log(kTag, "Dialogue IS playing. Current Prefab is: " + currentPrefab.name);
                currentPrefab.GetComponent<Outline>().enabled = true;
            }

            // Set up use of raycasts
            RaycastHit hit;
            Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            // Check for touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                ShowTrackingInfo(status, pose, Input.touchCount, touch.position.ToString());
                if (Physics.Raycast(raycast, out hit) && touch.phase == TouchPhase.Began && !DialogueManager.GetInstance().dialogueIsPlaying)
                {
                    // This is used to check if raycast on touch is working
                    OutputText.text = OutputText.text + "HIT \n";
                    if (hit.collider.tag == "TouchPrefab")
                    {
                        GameObject myObj = hit.collider.gameObject;
                        currentPrefab = myObj;
                        OutputText.text = OutputText.text + "You touched " + myObj.name + " \n";
                        myObj.transform.rotation = Quaternion.LookRotation(new Vector3(-Camera.main.transform.forward.x, myObj.transform.forward.y, -Camera.main.transform.forward.z));
                        toggleDialogueBox(myObj.name);
                    }

                }
                else
                {
                    OutputText.text = OutputText.text + "MISS \n";
                }
            }
        }

        void ShowTrackingInfo(string status, GeospatialPose pose, int touchCount = 0, string touchPosition = "")
        {
            // Displays Lat, Lng, Alt and their position
            OutputText.text = string.Format(
                "Latitude/Longitude: {0}°, {1}°\n" +
                "Horizontal Accuracy: {2}m\n" +
                "Altitude: {3}m\n" +
                "Vertical Accuracy: {4}m\n" +
                "Heading: {5}°\n" +
                "Heading Accuracy: {6}°\n" +
                "{7} \n" +
                "Touch Count: {8} \n" +
                "Touch Position: {9} \n"
                ,
                pose.Latitude.ToString("F6"),  //{0}
                pose.Longitude.ToString("F6"), //{1}
                pose.HorizontalAccuracy.ToString("F6"), //{2}
                pose.Altitude.ToString("F2"),  //{3}
                pose.VerticalAccuracy.ToString("F2"),  //{4}
                pose.Heading.ToString("F1"),   //{5}
                pose.HeadingAccuracy.ToString("F1"),   //{6}
                status, //{7},
                touchCount.ToString(),
                touchPosition
            );
        }

        void toggleDialogueBox(String prefabName)
        {
            // Create different dialogue each prefab location. We can choose which dialogue box to display based on name of prefab
            // GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("DialogueBox").gameObject.SetActive(true);
            // GameObject myCanvas = GameObject.Find("Canvas").GetComponent<Canvas>().gameObject;

            myLogger.Log(kTag, "Toggling DialogueBox...");
            myLogger.Log(kTag, "Attempting to access Dialogue Manager...");
            switch (prefabName)
            {
                case "myHomeObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_default);
                    break;
                case "ecsObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_ecs);
                    break;
                case "tsuObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_tsu);
                    break;
                case "titanShopsObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_titanShops);
                    break;
                case "healthCenterObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_healthCenter);
                    break;
                case "pollakLibraryObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_pollakLibrary);
                    break;
                case "humanitiesObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_humanities);
                    break;
                case "mihayloObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_mihaylo);
                    break;
                case "mccarthyObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_mccarthy);
                    break;
                case "visualArtsObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_visualArts);
                    break;
                case "recCenterObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_recCenter);
                    break;
                case "khsObj":
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_khs);
                    break;
                default:
                    //DialogueManager.GetInstance().EnterDialogueMode(inkJSON_default);
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON_default);
                    break;
            }
        }

        // Function to instantiate our prefab
        void instantiatePrefab(ref GameObject displayObject, prefabLocation prefabInfo, GeospatialPose pose, String prefabName)
        {
            if (displayObject == null)
            {
                //Height of the phone - 2m to be approximately the height of the ground
                Altitude = pose.Altitude - 2f;
                // Altitude = 35f;

                // Angle correction (Because the anchor generation function assumes South=0)
                Quaternion quaternion = Quaternion.AngleAxis(180f - (float)Heading, Vector3.up);

                // Create anchors at specified position and orientation.
                ARGeospatialAnchor Anchor = AnchorManager.AddAnchor(prefabInfo.Latitude, prefabInfo.Longitude, Altitude, quaternion);

                // Materialize the object if the anchor is correctly created
                if (Anchor != null)
                {
                    myLogger.Log(kTag, "instantiating: " + prefabName);
                    displayObject = Instantiate(ContentPrefab, Anchor.transform);
                    displayObject.name = prefabName;
                    displayObject.SetActive(true);
                    // displayObject.GetComponent<Outline>().enabled = false;  

                    // Add outline. UNUSED because we already added it onto the prefab through Unity. Use code if prefab does not have outline.
                    // var outline = displayObject.AddComponent<Outline>();
                    // outline.OutlineMode = Outline.Mode.OutlineAll;
                    // outline.OutlineColor = Color.white;
                    // outline.OutlineWidth = 10f;
                    // outline.enabled = true;
                }
            }
        }
    }
}



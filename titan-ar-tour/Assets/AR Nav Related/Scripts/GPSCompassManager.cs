using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class GPSCompassManager : MonoBehaviour
{
    public Transform player; // Player object to update position and rotation
    public float maxDistance = 50f; // Maximum distance for audio representation
    public float updateRate = 5f; // Update rate for processing audio sources

    [Header("Compass Parameters")]
    public Camera mainCamera; // Reference to the main camera for compass calculation
    public RawImage compassImage; // UI element to display the compass
    public GameObject compassMarkerPrefab; // Prefab for compass markers
    public float compassUpdateInterval = 0.01f; // Interval for updating the compass

    private List<AudioSource> audioSources = new List<AudioSource>(); // List of audio sources in the scene

    // GPS and Compass data
    private bool isGPSInitialized = false;
    private float gpsUpdateInterval = 1.0f; // Interval for updating GPS data

    void Start()
    {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("GPS not enabled by user");
            yield break;
        }

        Input.location.Start();
        Input.compass.enabled = true;

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("GPS service initialization failed");
            yield break;
        }

        isGPSInitialized = true;
        StartCoroutine(UpdateGPSData());
    }

    private IEnumerator UpdateGPSData()
    {
        while (isGPSInitialized)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                // Update player position based on GPS data
                // Convert GPS latitude and longitude to Unity world coordinates as needed
                // player.position = ConvertGPSToUnityCoordinates(Input.location.lastData.latitude, Input.location.lastData.longitude);

                // Update compass rotation
                compassImage.uvRect = new Rect(Input.compass.trueHeading / 360f, 0f, 1f, 1f);

                // Additional functionality to handle audio sources can be added here
                UpdateAudioSources();
            }
            yield return new WaitForSeconds(gpsUpdateInterval);
        }
    }

    void UpdateAudioSources()
    {
        foreach (AudioSource source in audioSources)
        {
            if (Vector3.Distance(player.position, source.transform.position) <= maxDistance)
            {
                // Process each audio source. Implement as per your game logic
            }
        }
    }

    void OnDestroy()
    {
        if (isGPSInitialized)
            Input.location.Stop();
    }

    // Implement this method based on your game's coordinate system and scale
    Vector3 ConvertGPSToUnityCoordinates(float latitude, float longitude)
    {
        // Convert GPS coordinates to Unity world coordinates
        // This is highly dependent on your game's mapping of the real world to the game world
        return new Vector3(longitude, 0, latitude);
    }
}
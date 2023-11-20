using UnityEngine;

public class TestActivation : MonoBehaviour
{
    public GameObject targetObject;

    void Start()
    {
        // Call SetActive(true) directly in Start for testing purposes
        if (targetObject != null)
        {
            targetObject.SetActive(true);
            Debug.Log("Target object activated outside dropdown script.");
        }
        else
        {
            Debug.LogError("Target object not found or not assigned.");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaction_Blocker : MonoBehaviour
{
    public TMP_Dropdown Dropdown;
    private CanvasGroup canvas_dropdown;
    // Start is called before the first frame update
    void Start()
    {
        canvas_dropdown = GetComponent<CanvasGroup>();

        if (canvas_dropdown == null)
        {
            Debug.LogError("CanvasGroup not found on the parent GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas_dropdown != null)
        {
            if (canvas_dropdown.alpha == 0f) // CanvasGroup is closed
            {
                canvas_dropdown.alpha = 1f; // Make it visible
                canvas_dropdown.blocksRaycasts = true; // Enable raycasts
                Debug.Log("Dropdown Opened");
            }
            else // CanvasGroup is open
            {
                canvas_dropdown.alpha = 0f; // Make it invisible
                canvas_dropdown.blocksRaycasts = false; // Disable raycasts
                Debug.Log("Dropdown Closed");
            }
        }
    }
}

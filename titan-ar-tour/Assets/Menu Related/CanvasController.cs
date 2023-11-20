using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasToControl;

    public void ToggleCanvas()
    {
        Debug.Log("clicked exit button");
        canvasToControl.SetActive(!canvasToControl.activeSelf);
    }
}

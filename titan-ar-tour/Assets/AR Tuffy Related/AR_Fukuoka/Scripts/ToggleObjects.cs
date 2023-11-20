using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;

    void Update()
    {
        if (objectA != null && objectB != null)
        {
            if (objectA.activeSelf)
            {
                objectB.SetActive(false);
            }
            else
            {
                objectB.SetActive(true);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Button Button;
    public string SceneName;
    public string DestinationName;

    void Start()
    {
        Button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        // Set the destination using PlayerPrefs before switching scenes
        PlayerPrefs.SetString("Destination", DestinationName);

        // Now load the target scene
        SceneManager.LoadScene(SceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay(5));
    }

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Assuming scenes are ordered sequentially in the build settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Load the next scene
        SceneManager.LoadScene("Menu");
    }
}

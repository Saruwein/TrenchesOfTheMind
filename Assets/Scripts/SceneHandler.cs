using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    /// <summary>
    /// Switch scene
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(string sceneName)
    {
        // Load the scene with the specified name
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Quit application
    /// </summary>
    public void Quit()
    {
        // Check if we are in the editor
        #if UNITY_EDITOR
            // Stop playing the scene in the editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the application
            Application.Quit();
        #endif
    }
}
    



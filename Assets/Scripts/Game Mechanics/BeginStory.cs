using UnityEngine;

public class BeginStory : MonoBehaviour
{
    /// <summary>
    /// This is the first Storypoint, and it is passed on Start()
    /// </summary>
    void Start()
    {
        GetComponent<StoryPoint>()?.True();
    }
}

using UnityEngine;

public class DebugCollection : MonoBehaviour
{
    public void ToggleSongAvailabilities()
    {
        bool debug = true;
        for (int i = 0; i < Collection.collection.Count; i++)
        {
            Collection.collection[i] = !Collection.collection[i];
            debug = debug && Collection.collection[i];
        }
        Debug.Log($"Set all Songs {debug}");
    }
}

using UnityEngine;

public class JigSawFragment : MonoBehaviour
{
    private bool _isSelected = false;

    public void OnMouseDown() =>  _isSelected = true;
    public void OnMouseUp() => _isSelected = false;

    // Update is called once per frame
    void Update()
    {
        if (_isSelected) { transform.position = Input.mousePosition; }
    }
}

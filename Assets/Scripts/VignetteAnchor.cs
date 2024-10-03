using UnityEngine.UI;
using UnityEngine;
using Unity.Mathematics;

public class VignetteAnchor : MonoBehaviour
{
    public GameObject vignette;
    private RectTransform _transform;
    private Material _vignette;
    
    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _vignette = vignette.GetComponent<Image>().material;

        vignette.gameObject.SetActive(true);
        if (_vignette is null) { Debug.Log("vignette 404"); }
    }

    void Update()
    {
        // float2 center = new float2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
        _vignette.SetVector("_Center", Input.mousePosition);
    }
}

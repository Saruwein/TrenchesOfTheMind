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

        // Pass screen resolution to the shader
        _vignette.SetVector("_ScreenResolution", new Vector2(_transform.rect.width, _transform.rect.height));
    }

    /// <summary>
    /// send vignette radii drom min to max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public void Vignette(float min,  float max)
    {
        _vignette.SetFloat("Min Radius", min);
        _vignette.SetFloat("Max Radius", max);
    }

    void Update()
    {
        Vector2 uvCenter = new Vector2(
            (Input.mousePosition.x / Screen.width),
            (Input.mousePosition.y / Screen.height)
        );

        _vignette.SetVector("_Center", uvCenter);
    }

}

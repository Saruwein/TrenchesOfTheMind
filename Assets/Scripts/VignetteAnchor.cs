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
        VignetteSmooth(300);
    }

    /// <summary>
    /// very smooth gradient where min = 10% * max
    /// </summary>
    /// <param name="max"></param>
    public void VignetteSmooth(float max) => Vignette(max * .10f ,max);
    /// <summary>
    /// slim gradient where min = 50% * max
    /// </summary>
    /// <param name="max"></param>
    public void VignetteAverage(float max) => Vignette(max * .5f, max);
    /// <summary>
    /// hard gradient where min = 85% * max
    /// </summary>
    /// <param name="max"></param>
    public void VignetteNette(float max) => Vignette(max * .85f, max);

    /// <summary>
    /// set vignette size
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public void Vignette(float min, float max)
    {
        _vignette.SetFloat("_MinRadius", min);
        _vignette.SetFloat("_MaxRadius", max);
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

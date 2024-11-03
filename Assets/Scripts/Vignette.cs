using UnityEngine.UI;
using UnityEngine;

public class Vignette : MonoBehaviour
{
    public GameObject mask;
    public RectTransform anchor;
    public bool hasVideo = false;
    public RenderTexture rtext;

    private Material _vignette;
    

    private void Awake()
    {
        _vignette = mask.GetComponent<Image>().material;

        mask.gameObject.SetActive(true);
        if (_vignette is null) { Debug.Log("vignette 404"); }

        // Pass screen resolution to the shader
        _vignette.SetVector("_ScreenResolution", new Vector2(anchor.rect.width, anchor.rect.height));
        if (hasVideo) { _vignette.SetTexture("_RenderTex", rtext); }
        
        // initialize vignettte to size 300 with smooth gradiant
        Smooth(300);
    }

    /// <summary>
    /// very smooth gradient where min = 10% * max
    /// </summary>
    /// <param name="max">vignette size</param>
    public void Smooth(float max) => VignetteRadii(max * .10f ,max);
    /// <summary>
    /// slim gradient where min = 50% * max
    /// </summary>
    /// <param name="max">vignette size</param>
    public void Average(float max) => VignetteRadii(max * .5f, max);
    /// <summary>
    /// hard gradient where min = 85% * max
    /// </summary>
    /// <param name="max">vignette size</param>
    public void Nette(float max) => VignetteRadii(max * .85f, max);

    /// <summary>
    /// set vignette size
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public void VignetteRadii(float min, float max)
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

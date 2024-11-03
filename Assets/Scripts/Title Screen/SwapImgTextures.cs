using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapImgTexture : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;

    public Sprite primary;
    public Sprite secondary;

    /// <summary>
    /// swap image onclick
    /// </summary>
    public void TextToggle()
    {
        image.sprite = (image.sprite == secondary) ? primary : secondary; 
    }
    /// <summary>
    /// displays primary tecture
    /// </summary>
    public void TextPrimary()
    {
        image.sprite = primary;
    }
    /// <summary>
    /// displays secondary texture
    /// </summary>
    public void TextSecondary()
    {
        image.sprite = secondary;
    }

    /// <summary>
    /// Trigger on pointer (mouse) enter
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) => image.sprite = secondary;

    /// <summary>
    /// Trigger on pointer (mouse) exit
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) => image.sprite = primary;
}

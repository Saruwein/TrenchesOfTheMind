using UnityEngine.UI;
using UnityEngine;

public class SwapImg : MonoBehaviour
{
    public Image image;

    public Sprite defaultText;
    public Sprite replacement;

    /// <summary>
    /// swap images on mouse hover
    /// </summary>
    public void OnMouseEnter()
    {
        image.sprite = replacement;
    }

    /// <summary>
    /// swap images on mousehover
    /// </summary>
    public void OnMouseExit()
    {
        image.sprite = defaultText;
    }

    /// <summary>
    /// swap image onclick
    /// should perform when web app accessed remotely
    /// </summary>
    public void OnMouseDown()
    {
        image.sprite = image.sprite == replacement ? defaultText : replacement; 
    }   
}

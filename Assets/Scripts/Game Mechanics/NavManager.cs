using UnityEngine;
using UnityEngine.UI;

public class NavManager : MonoBehaviour
{
    public Progress progress;
    [Space]
    public Button BtnLeft;
    public Button BtnRight;
    public Button BtnBack;

    /// <summary>
    /// handles nav arrow behavior
    /// </summary>
    public void ZoomIn()
    {
        BtnLeft?.gameObject.SetActive(false);
        BtnRight?.gameObject.SetActive(false);
        BtnBack?.gameObject.SetActive(true);
    }
    
    public void ZoomOut()
    {
        BtnLeft?.gameObject.SetActive(true);
        BtnRight?.gameObject.SetActive(true);
        BtnBack?.gameObject.SetActive(false);
    }

    public void MoveLeft() => progress.ScreenLeft();
    public void MoveRight() => progress.ScreenRight();


}

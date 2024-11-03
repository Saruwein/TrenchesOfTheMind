using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{

    public List<GameObject> Level0 = new List<GameObject>();

    public View Left;
    public View Right;
    public NavManager NavManager;

    public bool debug = true;

    private void OnEnable()
    {
        foreach (GameObject p in Level0) { gameObject.SetActive(true); }
        
        for (int i = 0; i < 2; i++)
        {
            // iterate through Left, then Right
            Button btn = i == 0 ? NavManager.BtnLeft : NavManager.BtnRight;
            View view = i == 0 ? Left : Right;
            if(debug) { Debug.Log($"View Left = {Left.name} // View Right = {Right.name}"); }

            // set Button Active and replace Listeners with View switch
            btn.gameObject.SetActive(true);
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => view.transform.parent.gameObject.SetActive(true));
            btn.onClick.AddListener(() => transform.parent.gameObject.SetActive(false));
        }
        
    }

    /// <summary>
    /// toggle visibility of Navigation Buttons
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleNavBtnVis(bool toggle)
    {
        if (debug) { Debug.Log($"NavButtons.SetActive({toggle})"); }
        NavManager.BtnLeft.gameObject.SetActive(toggle);
        NavManager.BtnRight.gameObject.SetActive(toggle);
    }





}

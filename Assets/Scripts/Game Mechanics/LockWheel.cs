using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockWheel : MonoBehaviour
{
    public Button tryLock;
    public Button top;
    public Button bot;

    public List<Sprite> symbols = new List<Sprite>();
    public int selected = 1;
    public int intended;


    /// <summary>
    /// shift LockWheel by 1 symbol
    /// </summary>
    public void UpWheel()
    {
        selected = selectUp(selected);
        Display(selected);
    }
    /// <summary>
    /// Shift Lockwheel by 1 symbol
    /// </summary>
    public void DownWheel()
    {
        selected = selectDown(selected);
        Display(selected);
    }


    private void Start()
    {
        Display(selected);
    }

    /// <summary>
    /// accurately display symbols
    /// </summary>
    /// <param name="select"></param>
    public void Display(int select)
    {
        // display selected (+1, -1) modulo symbols.Count
        gameObject.GetComponent<Image>().sprite = symbols[selected];
        top.image.sprite = symbols[selectUp(selected)];
        bot.image.sprite = symbols[selectDown(selected)];
    }

    /// <summary>
    /// if selected is too high, set 0, else raise one
    /// </summary>
    /// <param name="selected"></param>
    /// <returns></returns>
    public int selectUp(int selected) => (selected +1) % symbols.Count;
    /// <summary>
    /// if selected too low set Count -1, else reduce one
    /// </summary>
    /// <param name="selected"></param>
    /// <returns></returns>
    public int selectDown(int selected) => (selected + symbols.Count -1) % symbols.Count;
        

}

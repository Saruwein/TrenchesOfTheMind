using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Lock : MonoBehaviour
{
    public List<Sprite> symbolSprites = new List<Sprite>();
    public List<Symbol> symbolGOs = new List<Symbol>();
    [Space]
    public List<LockWheel> wheels = new List<LockWheel>();

    private int[] _lockSymbols = new int[3];


    private void Awake()
    {
        BuildLock();
    }

    /// <summary>
    /// returns whether or not door is locked
    /// </summary>
    /// <returns></returns>
    public bool isLocked()
    {
        bool l = false;
        string debug = "Lockstate: ";
        // iterate through wheels and check if intended == selected
        for (int i = 0; i < wheels.Count; i++)
        {
            debug += $"\nwheel[{i}] returns {wheels[i].selected != wheels[i].intended}";
            if (wheels[i].selected != wheels[i].intended) { l = true; break; }
        }
        Debug.Log(debug + $"\nLockstate {l}");
        return l;
    }


    /// <summary>
    /// draw three unique characters 
    /// </summary>
    private void BuildLock()
    {
        _lockSymbols[0] = RandomLock();
        do { _lockSymbols[1] = RandomLock(); } while (_lockSymbols[1] == _lockSymbols[0]);
        do { _lockSymbols[2] = RandomLock(); } while (_lockSymbols[2] == _lockSymbols[0] || _lockSymbols[2] == _lockSymbols[1]);
    }

    /// <summary>
    /// Draw random number fromlocks
    /// </summary>
    /// <returns></returns>
    private int RandomLock()
    {
        double d = Random.Range(0, 1);
        return (int)Mathf.Round((float)d * symbolSprites.Count - 1);
    }


    public void DistributeSymbols()
    {
        List<int> symbols = new List<int>();
        for (int i = 0; i < 3; i++) { symbols.Add(_lockSymbols[i]); }

        for (int i = 3; i < symbolGOs.Count; i++) 
        {
            int symbol;
            do { symbol = RandomLock(); } while (_lockSymbols.Contains(symbol));            
            symbols.Add(symbol); 
        }
        // get random for each SymbolGO
        for (int i = 0; i < symbolGOs.Count; i++)
        {
            int r = Random.Range(0, 1) * symbols.Count;
            // set sprite and mark relevant
            symbolGOs[i].GetComponent<Image>().sprite = symbolSprites[symbols[r]];
            if (_lockSymbols.Contains(symbols[r]))
            {
                symbolGOs[i].GetComponent<Image>().tintColor = new Color(0.647f, 0.0f, 0.059f);
            }
            symbols.RemoveAt(r);
        }
    }




}

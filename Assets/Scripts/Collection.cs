using System.Collections.Generic;

public static class Collection
{
    /// <summary>
    /// Ora
    /// </summary>
    public static bool collect_Jigsaw = false;
    /// <summary>
    /// Drifter
    /// </summary>
    public static bool collect_Knife = false;
    /// <summary>
    /// Breaking Point
    /// </summary>
    public static bool collect_AFrame = false;
    /// <summary>
    /// Almost There
    /// </summary>
    public static bool collect_Nut = false;
    /// <summary>
    /// Bauunterlage
    /// </summary>
    public static bool collect_ToiletPaper = false;
    /// <summary>
    /// Trenches
    /// </summary>
    public static  bool collect_Spider = false;
    /// <summary>
    /// Loathing
    /// </summary>
    public static bool collect_Money = false;

    public static List<bool> collection = new List<bool> 
    { 
        false,  // dummy
        collect_Jigsaw, 
        collect_Knife, 
        collect_AFrame,
        collect_Nut, 
        collect_ToiletPaper, 
        collect_Spider, 
        collect_Money 
    };

    /// <summary>
    /// check song at track number i
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static bool Check(int i)
    {
        return collection[i];
    }

}

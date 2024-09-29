using UnityEngine;


 [System.Flags]
 public enum Axis
 {
     None = 0,            // 000
     X = 1 << 0,         // 001
     Y = 1 << 1,         // 010
     Z = 1 << 2,         // 100
     All = X | Y | Z     // 111
 }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSettings
{
    public enum Difficulty { 
        Easy,
        Medium,
        Hard
    }
    public static Difficulty difficulty = Difficulty.Easy;
    public static bool success = false;
}

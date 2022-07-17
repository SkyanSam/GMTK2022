using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Point", menuName ="Grid Point")]
[System.Serializable]
public class Grid_Point : ScriptableObject
{
    public enum Point_Type
    {
        COMBAT_EASY,
        COMBAT_MEDIUM,
        COMBAT_HARD,
        START,
        FINISH
    }

    public Point_Type pointType;

    public Vector2 position;
}

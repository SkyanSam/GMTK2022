using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Point", menuName ="Grid Point")]
public class Grid_Point : ScriptableObject
{
    public enum Point_Type
    {
        COMBAT,
        LOOT
    }

    public Point_Type pointType;

    public Vector2 position;

    Transform transform;
}

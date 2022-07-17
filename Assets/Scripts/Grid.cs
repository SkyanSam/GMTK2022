using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;
    public List<Grid_Point> points = new List<Grid_Point>();
    private void Start()
    {
        Instance = this;
    }
}

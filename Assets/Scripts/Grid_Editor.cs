using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
[System.Serializable]
public class Grid_Editor : Editor
{
    Grid grid;
    Camera cam;

    private void OnEnable()
    {
        //The object with the grid script on it has to be called Grid Manager with that spelling and capitalization
        grid = GameObject.Find("Grid Manager").GetComponent<Grid>();
        //Make sure camera has default name
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void OnSceneGUI()
    {
        Event e = Event.current;

        //Draw and move gridpoints
        for (int i = 0; i < grid.points.Count; i++)
        {
            if (grid.points[i] != null)
            {
                Handles.color = Color.green;
                Vector2 newPos = Handles.FreeMoveHandle(grid.points[i].position, 0.5f, Vector2.zero, Handles.CylinderHandleCap);
                string gridType;

                if (grid.points[i].position != newPos)
                {
                    Undo.RecordObject(grid.gameObject, "Undo Move Point");
                    grid.points[i].position = newPos;
                }

                //Label points with their type
                //Add one statement for each grid type
                if (grid.points[i].pointType == Grid_Point.Point_Type.COMBAT)
                {
                    gridType = "Combat";
                }
                else
                {
                    gridType = "Loot";
                }
                Handles.Label(grid.points[i].position + new Vector2(0, 1), gridType);

                //Draw line to show grid order
                if(i > 0)
                {
                    Debug.DrawLine(grid.points[i - 1].position, grid.points[i].position, Color.red);
                }
            }
        }
    }
}

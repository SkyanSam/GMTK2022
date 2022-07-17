using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public string startLevelName;
    public string[] levelSceneNames;
    public void GoToLevel()
    {
        var pointType = Grid.Instance.points[Overworld_Player.currentPointIndex].pointType;
        if (pointType == Grid_Point.Point_Type.START) GlobalSettings.difficulty = GlobalSettings.Difficulty.Easy;
        if (pointType == Grid_Point.Point_Type.COMBAT_EASY) GlobalSettings.difficulty = GlobalSettings.Difficulty.Easy;
        if (pointType == Grid_Point.Point_Type.COMBAT_MEDIUM) GlobalSettings.difficulty = GlobalSettings.Difficulty.Medium;
        if (pointType == Grid_Point.Point_Type.COMBAT_HARD) GlobalSettings.difficulty = GlobalSettings.Difficulty.Hard;


        if (pointType == Grid_Point.Point_Type.START)
        {
            SceneManager.LoadScene(startLevelName);
        }
        else if (pointType == Grid_Point.Point_Type.FINISH)
        {
            Overworld_Player.currentPointIndex = 0; // reset point index for next game
            SceneManager.LoadScene("M_Ending");
        }
        else
        {
            var selectedLevelIndex = Random.Range(0, levelSceneNames.Length);
            SceneManager.LoadScene(levelSceneNames[selectedLevelIndex]);
        }
    }
}

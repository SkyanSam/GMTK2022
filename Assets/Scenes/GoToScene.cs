using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToAScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}

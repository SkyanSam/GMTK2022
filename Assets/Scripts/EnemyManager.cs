using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject success;
    void Start()
    {
        success.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var allEnemiesDead = true;
        foreach (var e in enemies)
        {
            if (e != null) allEnemiesDead =false;
        }
        if (allEnemiesDead)
        {
            StartCoroutine("Success");
        }
    }
    public IEnumerator Success()
    {
        success.SetActive(true);
        GlobalSettings.success = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Overworld");
    }
}

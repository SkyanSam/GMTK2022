using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCooldownSettings : MonoBehaviour
{
    BulletSpawner bulletSpawner;
    public float easyCooldown;
    public float mediumCooldown;
    public float hardCooldown;
    private void Start()
    {
        bulletSpawner = GetComponent<BulletSpawner>();
    }
    public void Update()
    {
        switch(GlobalSettings.difficulty)
        {
            case GlobalSettings.Difficulty.Hard:
                bulletSpawner.cooldown = hardCooldown; break;
            case GlobalSettings.Difficulty.Medium:
                bulletSpawner.cooldown = mediumCooldown; break;
            case GlobalSettings.Difficulty.Easy:
                bulletSpawner.cooldown = easyCooldown; break;
        }
    }
}

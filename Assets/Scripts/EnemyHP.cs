using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public GameObject healthBar;
    GameObject healthBarInner;
    public int easyStartHP;
    public int mediumStartHP;
    public int hardStartHP;
    public int totalHP 
    {
        get
        {
            switch (GlobalSettings.difficulty)
            {
                case GlobalSettings.Difficulty.Easy: return easyStartHP;
                case GlobalSettings.Difficulty.Medium: return mediumStartHP;
                case GlobalSettings.Difficulty.Hard: return hardStartHP;
                default: return mediumStartHP;
            }
        }
    }
    public float hitCooldownTotal;
    float hitCooldownRemaining;
    int currentHP;
    void Start()
    {
        if (healthBar == null) healthBar = transform.Find("HealthBar").gameObject;
        healthBarInner = healthBar.transform.Find("HealthBarInner").gameObject;
        currentHP = totalHP;
    }

    // Update is called once per frame
    void Update()
    {
        hitCooldownRemaining -= Time.deltaTime;
    }
    void UpdateHealthBar()
    {
        var tempScale = healthBarInner.transform.localScale;
        tempScale.x = Mathf.Lerp(0, 0.9f, (float)currentHP / totalHP);
        healthBarInner.transform.localScale = tempScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack" && hitCooldownRemaining <= 0)
        {
            hitCooldownRemaining = hitCooldownTotal;
            currentHP -= 1;
            if (currentHP == 0)
                Destroy(gameObject);
            UpdateHealthBar();
        }
    }
    
}

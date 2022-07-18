using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int startHp;
    int hp;
    public float bulletCooldown;
    float bulletTimer;
    GameObject healthBar;
    GameObject healthBarInner;
    void Start()
    {
        Instance = this;
        hp = startHp;
        if (healthBar == null) healthBar = transform.Find("HealthBar").gameObject;
        healthBarInner = healthBar.transform.Find("HealthBarInner").gameObject;
    }
    void Update()
    {
        bulletTimer -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && bulletTimer <= 0 && !PlayerSword.Instance.usingSword)
        {
            hp -= 1;
            Destroy(collision.gameObject);
            AudioManager.current.PlaySound("Hit");
            print(hp);
            bulletTimer = bulletCooldown;
            UpdateHealthBar();
            if (hp <= 0)
            {
                SceneManager.LoadScene("Overworld");
            }
        }
    }
    void UpdateHealthBar()
    {
        var tempScale = healthBarInner.transform.localScale;
        tempScale.x = Mathf.Lerp(0, 0.9f, (float)hp / startHp);
        healthBarInner.transform.localScale = tempScale;
    }
}

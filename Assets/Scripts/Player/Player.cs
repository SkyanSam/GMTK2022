using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int startHp;
    int hp;
    public float bulletCooldown;
    float bulletTimer;
    void Start()
    {
        Instance = this;
        hp = startHp;
    }
    void Update()
    {
        bulletTimer -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && bulletTimer <= 0)
        {
            hp -= 1;
            print(hp);
            bulletTimer = bulletCooldown;
        }
    }
}

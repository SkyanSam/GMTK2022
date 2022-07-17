using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletThree : Bullet
{
    bool isThreeSpawned = false;
    public float threeSpawnTime;
    public float threeRotationDifference = 45;
    public List<Bullet> childs;
    public new void Update()
    {
        if (startLifetime - currLifetime >= threeSpawnTime && !isThreeSpawned)
        {
            var b1 = Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<Bullet>();
            var b2 = Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<Bullet>();
            b1.rotation = rotation + threeRotationDifference;
            b2.rotation = rotation - threeRotationDifference;
            childs.Add(b1);
            childs.Add(b2);
            isThreeSpawned = true;
        }
        base.Update();
    }
    public override void OnBulletLifetimeOver()
    {
        foreach (Bullet b in childs)
        {
            b.OnBulletLifetimeOver();
            if (b != null)
                Destroy(b.gameObject);
        }
    }
}

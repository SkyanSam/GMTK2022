using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoming : Bullet
{
    public float homingTransitionSpeed;
    public float targetRotation;
    new public void Update()
    {
        var playerBulletVector = (Player.Instance.transform.position - transform.position).normalized;
        targetRotation = Mathf.Atan2(playerBulletVector.y, playerBulletVector.x) * Mathf.Rad2Deg;
        //rotation = Mathf.MoveTowards(rotation, targetRotation, homingTransitionSpeed * Time.deltaTime);
        transform.rotation = Vector4.MoveTowards(transform.rotation.ToVector4(), Quaternion.Euler(0, 0, targetRotation).ToVector4(), homingTransitionSpeed * Time.deltaTime).ToQuaternion();
        base.Update();
    }
}
public static class BulletHomingConvert { 
    public static Quaternion ToQuaternion(this Vector4 v)
    {
        return new Quaternion(v.x, v.y, v.z, v.w);  
    }
    public static Vector4 ToVector4(this Quaternion q)
    {
        return new Vector4(q.x, q.y, q.z, q.w);
    }
}


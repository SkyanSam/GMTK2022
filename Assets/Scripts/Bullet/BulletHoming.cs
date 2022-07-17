using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoming : Bullet
{
    public float steerForce;
    public float targetRotation;
    public Vector2 acceleration;
    new public void Start()
    {
        base.Start();
        useOnlyVelocity = true;
    }
    new public void Update()
    {
        var desired = (Vector2)(Player.Instance.transform.position - transform.position).normalized;
        acceleration = (desired - velocity).normalized * steerForce;
        velocity += acceleration * Time.deltaTime;
        //velocity = Vector2.ClampMagnitude(velocity, 1);
        velocity = velocity.normalized;
        rotation = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
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


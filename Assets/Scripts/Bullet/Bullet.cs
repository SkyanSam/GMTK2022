using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public float startLifetime = 10;
    float currLifetime;
    public bool useOnlyVelocity = false;
    public void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        currLifetime = startLifetime;
    }
    public void Update()
    {
        if (useOnlyVelocity) transform.position += (Vector3)velocity * speed * Time.deltaTime;
        else transform.Translate(velocity * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        currLifetime -= Time.deltaTime;
        if (currLifetime < 0) Destroy(gameObject);
    }
}

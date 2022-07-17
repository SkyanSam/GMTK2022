using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    BulletSpawner bulletSpawner;
    public float speed;
    public Vector2 startPos;
    public Vector2 endPos;
    SpriteRenderer spriteRenderer;
    public enum Mode { 
        GoToStart,
        GoToEnd,
    }
    public Mode mode;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletSpawner = transform.Find("BulletSpawner").GetComponent<BulletSpawner>();
        if (mode == Mode.GoToStart) transform.position = startPos;
        else transform.position = endPos;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.position == startPos && mode == Mode.GoToStart) mode = Mode.GoToEnd;
        else if ((Vector2)transform.position == endPos && mode == Mode.GoToEnd) mode = Mode.GoToStart;

        if (mode == Mode.GoToStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            spriteRenderer.flipX = endPos.x > startPos.x;
        }
        if (mode == Mode.GoToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            spriteRenderer.flipX = endPos.x < startPos.x;
        }

        if (spriteRenderer.flipX)
        {
            bulletSpawner.minRotation = bulletSpawner.maxRotation = 180;
        }
        else
        {
            bulletSpawner.minRotation = bulletSpawner.maxRotation = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(startPos, endPos);
        Gizmos.DrawSphere(startPos, 0.1f);
        Gizmos.DrawSphere(endPos, 0.25f);
    }
}

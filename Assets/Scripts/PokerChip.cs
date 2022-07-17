using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerChip : MonoBehaviour
{
    public float easySpeed;
    public float mediumSpeed;
    public float hardSpeed;
    public float speed;
    public Vector2 nextPos;
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        nextPos = transform.position;

        switch (GlobalSettings.difficulty)
        {
            case GlobalSettings.Difficulty.Hard:
                speed = hardSpeed; break;
            case GlobalSettings.Difficulty.Medium:
                speed = mediumSpeed; break;
            case GlobalSettings.Difficulty.Easy:
                speed = easySpeed; break;
        }
    }
    void Update()
    {
        if (nextPos == (Vector2)transform.position)
        {
            var newDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            var hit = Physics2D.Raycast(transform.position, newDir, 30, LayerMask.GetMask("ImInYourWalls"));
            nextPos = Vector2.Lerp(transform.position, (Vector2)transform.position + (hit.distance * newDir), Random.Range(0.3f, 0.7f));
            if (newDir.x < 0) spriteRenderer.flipX = false;
            else if (newDir.x > 0) spriteRenderer.flipX = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
}

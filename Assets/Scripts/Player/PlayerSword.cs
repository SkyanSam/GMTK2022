using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public static PlayerSword Instance;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider;
    public float cooldownTotalTime = 1;
    public float swingTime = 0.5f;
    float startAngle;
    float endAngle;
    public bool usingSword { get; private set; } = false;
    public float cooldownRemainingTime { get; private set; }
    void Start()
    {
        spriteRenderer = transform.Find("gfx").GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        Instance = this;
        DisableSword();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownRemainingTime <= 0)
        {
            AudioManager.current.PlaySound("Swing");
            cooldownRemainingTime = cooldownTotalTime;
            if (PlayerAnimation.Instance.directionFacing == PlayerAnimation.DirectionFacing.Left)
            {
                transform.parent.localScale = new Vector3(-1, 1, 1);
                startAngle = 270;
                endAngle = 90;
            }
            else
            {
                transform.parent.localScale = new Vector3(1, 1, 1);
                startAngle = 90;
                endAngle = 270;
            }
            transform.eulerAngles = new Vector3(0, 0, startAngle);
            EnableSword();
            var seq = LeanTween.sequence();
            seq.append(LeanTween.rotate(gameObject, new Vector3(0, 0, endAngle), swingTime));
            seq.append(() => DisableSword());
        }
        cooldownRemainingTime -= Time.deltaTime;
    }
    public void DisableSword()
    {
        usingSword = false;
        polygonCollider.enabled = false;
        spriteRenderer.enabled = false;
    }
    public void EnableSword()
    {
        usingSword = true;
        polygonCollider.enabled = true;
        spriteRenderer.enabled = true;
    }
}

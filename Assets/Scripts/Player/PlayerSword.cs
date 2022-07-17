using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float cooldownTotalTime = 1;
    public float swingTime = 0.5f;
    float startAngle;
    float endAngle;
    public float cooldownRemainingTime { get; private set; }
    void Start()
    {
        spriteRenderer = transform.Find("gfx").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownRemainingTime <= 0)
        {
            if (PlayerAnimation.Instance.directionFacing == PlayerAnimation.DirectionFacing.Left)
            {
                //spriteRenderer.flipX = true;
                var tempPos = transform.localPosition;
                tempPos.x = -Mathf.Abs(tempPos.x);
                transform.localPosition = tempPos;
                startAngle = 90;
                endAngle = -90;
            }
            else
            {
                spriteRenderer.flipX = false;
                var tempPos = transform.localPosition;
                tempPos.x = Mathf.Abs(tempPos.x);
                transform.localPosition = tempPos;
                startAngle = 90;
                endAngle = 270;
            }
            transform.eulerAngles = new Vector3(0, 0, startAngle);
            spriteRenderer.enabled = true;
            var seq = LeanTween.sequence();
            seq.append(LeanTween.rotate(gameObject, new Vector3(0, 0, endAngle), swingTime));
            seq.append(() => spriteRenderer.enabled = false);
        }
    }
}

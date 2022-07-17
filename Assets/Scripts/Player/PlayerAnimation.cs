using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance;
    public enum DirectionFacing
    {
        Left,
        Right
    }
    public DirectionFacing directionFacing { private set; get; }
    CustomAnimationPlayer animationPlayer;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        Instance = this;
        animationPlayer = GetComponent<CustomAnimationPlayer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.Instance.velocity == Vector2.zero && animationPlayer.currentAnimation != "Idle") animationPlayer.PlayAnimation("Idle");
        else if (PlayerMovement.Instance.velocity != Vector2.zero && animationPlayer.currentAnimation != "Walk") animationPlayer.PlayAnimation("Walk");
        if (PlayerMovement.Instance.velocity.x < 0 && !spriteRenderer.flipX) spriteRenderer.flipX = true;
        if (PlayerMovement.Instance.velocity.x > 0 && spriteRenderer.flipX) spriteRenderer.flipX = false;
        directionFacing = spriteRenderer.flipX ? DirectionFacing.Left : DirectionFacing.Right;
    }
}

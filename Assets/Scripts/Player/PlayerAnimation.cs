using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.Instance.velocity == Vector2.zero) animator.SetInteger("Mode", 0);
        else animator.SetInteger("Mode", 1);
        if (PlayerMovement.Instance.velocity.x < 0 && !spriteRenderer.flipX) spriteRenderer.flipX = true;
        if (PlayerMovement.Instance.velocity.x > 0 && spriteRenderer.flipX) spriteRenderer.flipX = false;
    }
}

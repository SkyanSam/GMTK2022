using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimationPlayer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public CustomAnimation[] animations;
    public int currentIndex;
    public string currentAnimation
    {
        get
        {
            return animations[currentIndex].name;
        }
    }
    public int currentFrame;
    public float frameInterval;
    float timeLeftOnThisFrame;
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateFrame();
    }
    private void Update()
    {
        if (timeLeftOnThisFrame <= 0)
        {
            timeLeftOnThisFrame = frameInterval;
            currentFrame = currentFrame + 1 >= animations[currentIndex].frames.Length ? 0 : currentFrame + 1;
            UpdateFrame();
        }
        timeLeftOnThisFrame -= Time.deltaTime;
    }
    public void UpdateFrame()
    {
        spriteRenderer.sprite = animations[currentIndex].frames[currentFrame];
    }
    public void PlayAnimation(string name)
    {
        for (int i = 0; i < animations.Length; i++)
        {
            if (animations[i].name == name)
            {
                currentIndex = i;
                currentFrame = 0;
                timeLeftOnThisFrame = frameInterval;
                UpdateFrame();
                return;
            }
        }
        Debug.LogWarning("Animation " + name + " not found.");
        
    }
}
[System.Serializable]
public class CustomAnimation
{
    public string name;
    public Sprite[] frames;
}

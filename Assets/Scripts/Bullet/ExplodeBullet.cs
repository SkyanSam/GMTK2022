using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExplodeBullet : BulletHoming
{
    public ParticleSystem explosionGFX;
    public SpriteRenderer spriteRenderer;
    public float minHeartBeat;
    public float maxHeartBeat;
    public float beatScale;
    float timeTillNextHeartBeat;
    public float explosionSeconds = 1;
    Vector2 normalSpriteRendererScale;
    bool startedExplosion = false;
    
    public new void Start()
    {
        base.Start();
        normalSpriteRendererScale = spriteRenderer.transform.localScale;
    }
    public new void Update()
    {
        if (timeTillNextHeartBeat <= 0)
        {
            timeTillNextHeartBeat = Mathf.Lerp(minHeartBeat, maxHeartBeat, currLifetime / startLifetime);
            spriteRenderer.transform.localScale = normalSpriteRendererScale * beatScale;
            LeanTween.scale(spriteRenderer.gameObject, normalSpriteRendererScale, 0.5f * timeTillNextHeartBeat).setEaseOutCirc();
        }
        timeTillNextHeartBeat -= Time.deltaTime;
        
        if (!startedExplosion)
            base.Update(); // so doesnt update during explosion
    }
    public override void OnBulletLifetimeOver()
    {
        if (!startedExplosion)
        {
            startedExplosion = true;
            spriteRenderer.enabled = false;
            explosionGFX.Play();
            StartCoroutine(Explosion());
        }
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionSeconds);
        Destroy(gameObject);
    }
}

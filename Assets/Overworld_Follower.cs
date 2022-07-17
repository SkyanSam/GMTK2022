using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld_Follower : MonoBehaviour
{
    Animator animator;

    [Header("Jumping")]
    public float jumpSpeed = 0f;
    public float jumpHeight = 0f;

    public bool jumpRandomness = true;

    [Header("Following")]
    public float moveSpeed = 0f;
    public float closestDistance = 0f;

    Vector3 randomPlayerOffset = Vector3.zero;

    Transform followerSprite;

    Transform player;

    float jumpOffset;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Make sure the sprite being used is the first child of this object
        followerSprite = transform.GetChild(0);
        player = GameObject.Find("Player").GetComponent<Transform>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        randomPlayerOffset = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);

        if (jumpRandomness)
        {
            jumpOffset = Random.Range(0f, 3f);
            jumpSpeed += Random.Range(-0.5f, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Jumping stuff
        followerSprite.position = new Vector3(transform.position.x, Mathf.Clamp((Mathf.Sin((timer * jumpSpeed) + jumpOffset) * jumpHeight) + transform.position.y, transform.position.y, Mathf.Infinity));
        timer += Time.deltaTime;

        //Movement stuff
        if (followerSprite.position.y > transform.position.y)
        {
            Vector3 playerDist = (player.position + randomPlayerOffset) - transform.position;
            bool isMove = true;
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Follower"))
            {
                Vector3 followerDist = g.transform.position - transform.position;
                if (followerDist.magnitude < closestDistance && g != gameObject && Vector3.Angle(playerDist, g.transform.position - transform.position) < 45f)
                {
                    isMove = false;
                }
            }

            if (playerDist.magnitude > closestDistance && isMove)
            {
                transform.position += playerDist.normalized * moveSpeed * Time.deltaTime;
            }

            animator.speed = 1;

            //Set sprite layer order
            followerSprite.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y;
        }
        else
        {
            animator.speed = 0;
            randomPlayerOffset = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        }
    }
}

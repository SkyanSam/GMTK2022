using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld_Camera : MonoBehaviour
{
    public Transform player;

    public float followSpeed;
    
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.position.x, followSpeed * Time.deltaTime), Mathf.Lerp(transform.position.y, player.position.y, followSpeed * Time.deltaTime), transform.position.z);
    }
}

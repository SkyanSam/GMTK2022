using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Overworld_Player : MonoBehaviour
{
    public static Overworld_Player Instance;
    Grid grid;
    Transform playerSprite;

    [Header("Followers")]
    public GameObject follower;
    public int numFollowers;

    [HideInInspector]
    public static int currentPointIndex = 0;
    [Header("Movement Visuals")]
    int targetPointIndex = 0;
    int lastPointIndex = 0;
    public float moveSpeed = 0f;
    public float bounceHeight = 0f;

    [HideInInspector]
    public bool isMoving = false;

    [HideInInspector]
    public bool canReroll = false;

    List<int> pointsToEnd = new List<int>();

    void Start()
    {
        Instance = this;
        grid = GameObject.Find("Grid Manager").GetComponent<Grid>();
        playerSprite = transform.GetChild(0);
        transform.position = new Vector3(grid.points[currentPointIndex].position.x, grid.points[currentPointIndex].position.y, 0);
        SpawnFollowers();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        if ((pos - grid.points[targetPointIndex].position).magnitude > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            if (pointsToEnd.Count > 0)
            {
                lastPointIndex = pointsToEnd[0];
                pointsToEnd.Remove(pointsToEnd[0]);
            }
            if (pointsToEnd.Count == 0)
            {
                isMoving = false;
                if (currentPointIndex != 0 && !canReroll)
                {
                    canReroll = true;
                    grid.gameObject.GetComponent<Slot_Machine_Controller>().ChangeState(Slot_Machine_Controller.GameState.LEVEL_START);
                }
            }
            else
            {
                SetTarget(pointsToEnd[0]);
            }
        }

        if (isMoving)
        {
            MoveCharacter();
        }
        else
        {
            transform.position = grid.points[currentPointIndex].position;
        }

        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y;
    }

    public void ChangePoint(int newIndex)
    {
        canReroll = false;
        lastPointIndex = currentPointIndex;
        pointsToEnd = new List<int>();
        newIndex = Mathf.Clamp(newIndex, 0, grid.points.Count - 1);
        if (newIndex - currentPointIndex > 1)
        {
            for (int i = currentPointIndex + 1; i <= newIndex; i++)
            {
                pointsToEnd.Add(i);
            }
        }
        currentPointIndex = Mathf.Clamp(newIndex, 0, grid.points.Count - 1);
        if (pointsToEnd.Count > 0)
        {
            SetTarget(pointsToEnd[0]);
        }
        else
        {
            SetTarget(currentPointIndex);
        }
    }

    void SetTarget(int newTargetIndex)
    {
        targetPointIndex = newTargetIndex;
    }

     void MoveCharacter()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        float y = Mathf.Sin(Mathf.Deg2Rad * 180 * ((pos - grid.points[lastPointIndex].position).magnitude / (grid.points[targetPointIndex].position - grid.points[lastPointIndex].position).magnitude)) * bounceHeight;
        Vector2 newPos = (grid.points[targetPointIndex].position - pos).normalized * moveSpeed * Time.deltaTime;
        //Plug in for y: y + grid.points[lastPointIndex].position.y
        transform.position += new Vector3(newPos.x, newPos.y, 0);
        playerSprite.position = new Vector3(transform.position.x, Mathf.Clamp(y + transform.position.y, 0.11f, Mathf.Infinity), 0);
    }

    void SpawnFollowers()
    {
        for(int i = 0; i < numFollowers; i++)
        {
            Vector2 randomSpawn = new Vector2(transform.position.x, transform.position.y) + (Random.insideUnitCircle - new Vector2(transform.position.x, transform.position.y)).normalized * Random.Range(1f, 2f);
            Vector3 v = new Vector3(randomSpawn.x, randomSpawn.y, 0);
            Instantiate(follower, v, Quaternion.identity);
        }
    }
}

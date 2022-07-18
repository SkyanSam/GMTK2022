using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice_Manager : MonoBehaviour
{
    public static Dice_Manager instance;
    GameObject player;

    [Header("Dice Logic")]
    public float rollDuration = 1f;
    float diceTimer = 0;
    int diceNumber = 0;
    bool hasMoved = false;

    [Header("UI")]
    public Sprite[] diceFaces;
    public Image diceImage;
    float faceTimer = 0;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDiceUI();
    }

    public void RollDice()
    {
        if (!player.GetComponent<Overworld_Player>().isMoving && diceTimer <= 0)
        {
            hasMoved = false;
            diceTimer = rollDuration;
            diceNumber = Random.Range(1, 6);
        }
    }

    void UpdateDiceUI()
    {
        if (diceTimer > 0)
        {
            diceTimer -= Time.deltaTime;
            if(faceTimer <= 0)
            {
                faceTimer = 0.1f;
                diceImage.sprite = diceFaces[Random.Range(0, 5)];
            }
            else
            {
                faceTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (diceNumber == 1) diceImage.sprite = diceFaces[0];
            else if (diceNumber == 2) diceImage.sprite = diceFaces[1];
            else if (diceNumber == 3) diceImage.sprite = diceFaces[2];
            else if (diceNumber == 4) diceImage.sprite = diceFaces[3];
            else if (diceNumber == 5) diceImage.sprite = diceFaces[4];
            else if (diceNumber == 6) diceImage.sprite = diceFaces[5];

            //Move player if they have not already moved
            if (!hasMoved)
            {
                MovePlayer(diceNumber);
                hasMoved = true;
            }
        }
    }

    public void MovePlayer(int diceNum)
    {
        player.GetComponent<Overworld_Player>().ChangePoint(Overworld_Player.currentPointIndex + diceNum);
        //player.GetComponent<Overworld_Player>().ChangePoint(player.GetComponent<Overworld_Player>().currentPointIndex + diceNum);
    }
}

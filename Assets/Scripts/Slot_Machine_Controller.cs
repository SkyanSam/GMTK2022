using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Machine_Controller : MonoBehaviour
{
    public static Slot_Machine_Controller instance;
    GameObject player;

    public Canvas levelStartCanvas;
    public Canvas mainCanvas;
    public GameObject slotMachine;

    GameState currentState;

    public enum GameState
    {
        OVERWORLD,
        LEVEL_START,
        SLOT_MACHINE
    }

    void Start()
    {
        player = GameObject.Find("Player");
        instance = this;
        ChangeState(GameState.OVERWORLD);
        levelStartCanvas.gameObject.SetActive(false);
        slotMachine.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == GameState.SLOT_MACHINE)
        {
            UpdateSlotMachine();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndSlotMachine();
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        if (currentState != newState)
        {
            if (currentState == GameState.OVERWORLD)
            {
                ExitOverworldMenu();
            }
            else if (currentState == GameState.LEVEL_START)
            {
                ExitLevelStartMenu();
            }
        }

        currentState = newState;

        if (currentState == GameState.OVERWORLD)
        {
            OverworldMenu();
        }
        else if (currentState == GameState.LEVEL_START)
        {
            LevelStartMenu();
        }
    }

    public void LevelStartMenu()
    {
        Animator levelStartAnimator1 = levelStartCanvas.GetComponent<Animator>();

        levelStartCanvas.gameObject.SetActive(true);
        levelStartAnimator1.Play("Level_Begin_Anim1");
    }

    public void ExitLevelStartMenu()
    {
        Animator levelStartAnimator1 = levelStartCanvas.GetComponent<Animator>();

        levelStartAnimator1.Play("Level_Begin_Anim2");
    }

    public void OverworldMenu()
    {
        mainCanvas.gameObject.SetActive(true);
    }

    public void ExitOverworldMenu()
    {
        mainCanvas.gameObject.SetActive(false);
    }

    public void StartSlotMachine()
    {
        Animator slotMachineAnimator = slotMachine.GetComponent<Animator>();

        slotMachine.SetActive(true);
        slotMachineAnimator.Play("Slot_Machine_Fly_In_Bottom");

        win = Random.Range(0, 2) == 0 ? true : false;
        slotMachine.transform.GetChild(0).GetComponent<Animator>().SetBool("Win", win);

        ChangeState(GameState.SLOT_MACHINE);
    }

    bool win = false;
    public void UpdateSlotMachine()
    {
        if (slotMachine.transform.position.y == 4.51f)
        {
            slotMachine.transform.GetChild(0).GetComponent<Animator>().SetBool("StartRoll", true);
        }
    }

    public void EndSlotMachine()
    {
        slotMachine.transform.GetChild(0).GetComponent<Animator>().SetBool("StartRoll", false);
        slotMachine.transform.GetChild(0).GetComponent<Animator>().Play("Slot_Idle");
        Animator slotMachineAnimator = slotMachine.GetComponent<Animator>();
        var m_CurrentClipInfo = slotMachine.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        if (m_CurrentClipInfo[0].clip.name == "Slot_Lose")
        {
            //Overworld_Player.Instance.SetTargetBack();
            if (Overworld_Player.currentPointIndex > 3)
                Dice_Manager.instance.MovePlayer(-3);
            else if (Overworld_Player.currentPointIndex > 2)
                Dice_Manager.instance.MovePlayer(-2);
            else if (Overworld_Player.currentPointIndex > 1)
                Dice_Manager.instance.MovePlayer(-1);
        }
        slotMachineAnimator.Play("Slot_Machine_Fly_Out_Bottom");

        ChangeState(GameState.OVERWORLD);
    }
}

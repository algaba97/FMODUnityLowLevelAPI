using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputManager : MonoBehaviour
{
    public int cursorPosition;
    private Transform buttonArray;
    int cursorPositionMax;
    float cursorSpeed = 0.2f;
    float cursorTimer;

    void Start()
    {
        int cursorPosition = 0;
        buttonArray = GameObject.Find("UI_ButtonArray_MenuArray").transform;
        cursorPositionMax = buttonArray.childCount - 1;


    }

    void Update()
    {
        //detect input and move the cursos position
        cursorTimer += Time.deltaTime;
        if (Input.GetAxisRaw("Player1_Vertical_Left") < 0 && cursorTimer >= cursorSpeed)
        {
            cursorPosition++;
            if (cursorPosition > cursorPositionMax)
                cursorPosition = cursorPositionMax;

            cursorTimer = 0;
        }
        else if (Input.GetAxisRaw("Player1_Vertical_Left") > 0 && cursorTimer >= cursorSpeed)
        {
            cursorPosition--;
            if (cursorPosition < 0)
                cursorPosition = 0;

            cursorTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            ConsoleUIButton uIButton = buttonArray.GetChild(cursorPosition).GetComponent<ConsoleUIButton>();
            uIButton.OnClick();

        }
    }
}

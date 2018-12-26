using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuVisualController : MonoBehaviour
{
    private MenuInputManager menuInput;

    //visuals
    public GameObject ButtonParent;
    public RectTransform MenuCursor;

    private RectTransform[] menuButtons;

    void Update()
    {
        //make sure there is an input manager
        if (menuInput == null)
            menuInput = GetComponent<MenuInputManager>();
        else
        {
            if (menuButtons == null && ButtonParent != null)
            {
                menuButtons = ButtonParent.GetComponentsInChildren<RectTransform>();
                //remove the first entry since this is the parent object
                RectTransform[] temp = new RectTransform[menuButtons.Length - 1];
                for (int i = 1; i < menuButtons.Length; i++)
                {
                    temp[i - 1] = menuButtons[i];
                }

                menuButtons = temp;
            }

            else
            {
                if (MenuCursor != null)
                {
                    Vector2 cursorPos = new Vector2(menuButtons[menuInput.cursorPosition].position.x - 100, menuButtons[menuInput.cursorPosition].position.y);
                    MenuCursor.position = cursorPos;
                }
            }
        }
        //move cursor to currently selected button

    }
}

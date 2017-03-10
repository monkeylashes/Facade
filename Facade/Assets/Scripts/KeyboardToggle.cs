using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
public class KeyboardToggle : MonoBehaviour {
    [HideInInspector]
    public VRTK_ControllerEvents controller;
    private bool keyboardToggle = false;
    // Use this for initialization
    void Start () {
        SetEventHandlers();
	}
	
    protected virtual void SetEventHandlers()
    {
        if (controller == null)
        {
            controller = GetComponentInParent<VRTK_ControllerEvents>();            
        }
        
        // menu button
        controller.SubscribeToButtonAliasEvent(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress, false, MenuButtonAction);
        
    }

    protected void MenuButtonAction(object sender, ControllerInteractionEventArgs e)
    {
        GameObject keyboard = GetComponentInParent<PositionOut>().keyboard;
        Text textComponent = GetComponentInParent<PositionOut>().textComponent;

        textComponent.text = "Menu Button Pressed!";

        keyboardToggle = !keyboardToggle;
        keyboard.SetActive(keyboardToggle);
    }
}

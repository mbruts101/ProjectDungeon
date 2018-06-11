using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager //used to get inputs
{
    public static float MoveHorizontal()
    {
        float value = 0.0f;
        value += Input.GetAxis("Horizontal");
        value += Input.GetAxis("Joystick_Horizontal");
        return Mathf.Clamp(value, -1.0f, 1.0f); //makes sure value is not greater than 1 (so player cannot use keyboard and joystick to go faster)
    }

    public static float MoveVertical()
    {
        float value = 0.0f;
        value += Input.GetAxis("Vertical");
        value += Input.GetAxis("Joystick_Vertical");
        return Mathf.Clamp(value, -1.0f, 1.0f);
    }

    public static bool AttackButton()
    {
        return Input.GetButtonDown("Attack");
    }

    public static bool InteractButton()
    {
        return Input.GetButtonDown("Interact");
    }

    public static bool DodgeButton()
    {
        return Input.GetButtonDown("Dodge");
    }

}

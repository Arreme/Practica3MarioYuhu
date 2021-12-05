using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class RestartGame : MonoBehaviour
{
    private void Update()
    {
        bool keyReturn = false;
        if (Gamepad.current != null)
        {
            keyReturn = Gamepad.current.buttonNorth.wasPressedThisFrame;
        } else
        {
            keyReturn = Keyboard.current.enterKey.wasPressedThisFrame;
        }
        if (keyReturn)
        {
            SceneManager.LoadScene("ArremeScena");
        }
    }
}

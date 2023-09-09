using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoving : MonoBehaviour
{

// Simulate elevator button press using keyboard
void Update()
    {
        for (int i = 0; i <= 6; i++)
        {
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + i);
            KeyCode keypadKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Keypad" + i);

            if (Input.GetKeyDown(key) || Input.GetKeyDown(keypadKey))
            {
                Debug.Log("Tasto premuto: " + i);
                ButtonsManager.Instance.ActivateButton(i);
            }
        }
    }


}

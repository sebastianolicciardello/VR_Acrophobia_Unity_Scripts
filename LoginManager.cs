using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailInputField, passwordInputField;

    public void OnLoginClick(){
        string username = emailInputField.text;
        string password = passwordInputField.text;

        Debug.Log($"{username} : {password}");
    }
}

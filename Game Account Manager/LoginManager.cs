using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private string authenticationEndpoint = "http://127.0.0.1:13756/account";

    [SerializeField] private TMP_InputField usernameInputField, passwordInputField;
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private GameObject loginButton;

    public void OnLoginClick()
    {

        alertText.text = "Signing in...";
        loginButton.SetActive(false);

        StartCoroutine(TryLogin());
    }

    private IEnumerator TryLogin()
    {


        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if(username.Length < 3 || username.Length > 24)
        {
            alertText.text = "Invalid username";
            loginButton.SetActive(true);
            yield break;
        }

        if(password.Length < 3 || password.Length > 24)
        {
            alertText.text = "Invalid password";
            loginButton.SetActive(true);
            yield break;
        }


        UnityWebRequest request = UnityWebRequest.Get($"{authenticationEndpoint}?rUsername={username}&rPassword={password}");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f)
            {
                break;
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {

            if (request.downloadHandler.text != "Invalid credentials") //login success?
            {

                loginButton.SetActive(false);
                GameAccount returnedAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                alertText.text ="Welcome " + returnedAccount.username + ((returnedAccount.adminFlag == 1) ? " Admin" : "");
            } 
            else
            {
                alertText.text = "Invalid credentials";
                loginButton.SetActive(true);
            }

        }
        else
        {
            alertText.text = "Error connecting to the server";
            loginButton.SetActive(true);
        }

        yield return null;
    }
}

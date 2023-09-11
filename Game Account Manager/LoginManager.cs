using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LoginManager : MonoBehaviour
{
    private const string PASSWORD_REGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,24})";

    [SerializeField] private string loginEndpoint = "http://127.0.0.1:13756/account/login";

    [SerializeField] private TMP_InputField usernameInputField, passwordInputField;
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private GameObject loginButton;

    
    public delegate void OnLoginSuccess();
    public static event OnLoginSuccess onLoginSuccess;

    public void OnLoginClick()
    {

        alertText.text = "Signing in...";
        ActivateButtons(false);

        StartCoroutine(TryLogin());
    }

    private IEnumerator TryLogin()
    {


        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (username.Length < 3 || username.Length > 24)
        {
            alertText.text = "Invalid username";
            ActivateButtons(true);
            yield break;
        }

        if (!Regex.IsMatch(password, PASSWORD_REGEX))
        {
            alertText.text = "Invalid credentials";
            ActivateButtons(true);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);

        UnityWebRequest request = UnityWebRequest.Post(loginEndpoint, form);
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
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

            if (response.code == 0) //login success?
            {
                

                // Set the token in the PlayerPrefs
                PlayerPrefs.SetString("token", response.token);


                // login successful
                onLoginSuccess?.Invoke();

            }
            else
            {
                switch (response.code)
                {
                    case 1:
                        alertText.text = "Invalid credentials";
                        ActivateButtons(true);
                        break;
                    default:
                        alertText.text = "Corruption detected";
                        ActivateButtons(false);
                        break;

                }

            }

        }
        else
        {
            alertText.text = "Error connecting to the server";
            ActivateButtons(true);
        }

        yield return null;
    }

    private void ActivateButtons(bool toggle)
    {
        loginButton.SetActive(toggle);
    }
}

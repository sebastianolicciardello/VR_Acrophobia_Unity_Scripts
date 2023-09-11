using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject background, progressBar, login, players;


    void Start()
    {
        PlayerPrefs.DeleteAll();

        LoginManager.onLoginSuccess += ShowPlayers;

        // Execute after 1 second
        StartCoroutine(WaitAndExecute());
    }

    IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(0.1f);
        ButtonsManager.Instance.ActivateButton(1);

        yield return new WaitForSeconds(3);

        yield return StartCoroutine(Extensions.FadeOut(background));
        progressBar.SetActive(false);
        background.SetActive(false);

        login.SetActive(true);
        yield return StartCoroutine(Extensions.FadeIn(login));
    }

    private void ShowPlayers()
    {

        StartCoroutine(WaitPlayers());


    }

    IEnumerator WaitPlayers()
    {
        yield return StartCoroutine(Extensions.FadeOut(login));
        login.SetActive(false);


        players.SetActive(true);
        yield return StartCoroutine(Extensions.FadeIn(players));


    }

    



}
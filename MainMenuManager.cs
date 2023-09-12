
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject background, progressBar, login, players, playerSelected;

    [SerializeField] private TextMeshProUGUI alertText;



    void Start()
    {
        PlayerPrefs.DeleteAll();

        LoginManager.onLoginSuccess += ShowPlayers;

        PlayersManager.onPlayerSelected += ShowConfirmDialog;

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

private void ShowConfirmDialog(string id, string fullname)
    {
        PlayerPrefs.SetString("playerId", "");
        PlayerPrefs.SetString("playerName", "");

        // Use the id and fullname values
        alertText.text = fullname;
        PlayerPrefs.SetString("playerId", id);
        PlayerPrefs.SetString("playerName", fullname);


        playerSelected.SetActive(true);
        StartCoroutine(Extensions.FadeIn(playerSelected));
    }

    public void StartPlayerSession()
    { 
        Debug.Log(PlayerPrefs.GetString("playerId"));
        Debug.Log(PlayerPrefs.GetString("playerName"));
    }

    public void CloseConfirmDialog()
    {
        StartCoroutine(Extensions.FadeOut(playerSelected));
        playerSelected.SetActive(false);

    }

    



}
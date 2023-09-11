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

        yield return StartCoroutine(FadeOut(background));
        progressBar.SetActive(false);
        background.SetActive(false);

        login.SetActive(true);
        yield return StartCoroutine(FadeIn(login));
    }

    private void ShowPlayers()
    {

        StartCoroutine(WaitPlayers());


    }

    IEnumerator WaitPlayers()
    {
        yield return StartCoroutine(FadeOut(login));
        login.SetActive(false);


        players.SetActive(true);
        yield return StartCoroutine(FadeIn(players));


    }

    private IEnumerator FadeOut(GameObject panel)
    {
        // Recover the CanvasGroup component from the panel
        CanvasGroup canvasGroupPanel = panel.GetComponent<CanvasGroup>();

        // Start a fade-out animation for the panel
        float fadeOutDuration = 1.0f;
        float elapsedTimeFadeOut = 0.0f;

        while (elapsedTimeFadeOut < fadeOutDuration)
        {
            float alpha = 1 - (elapsedTimeFadeOut / fadeOutDuration);
            canvasGroupPanel.alpha = alpha;

            elapsedTimeFadeOut += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }
    }

    private IEnumerator FadeIn(GameObject panel)
    {
        // Recover the CanvasGroup component from the panel
        CanvasGroup canvasGroupPanel = panel.GetComponent<CanvasGroup>();

        // Start a fade-in animation for the panel
        float fadeInDuration = 1.0f;
        float elapsedTimeFadeIn = 0.0f;

        while (elapsedTimeFadeIn < fadeInDuration)
        {
            float alpha = elapsedTimeFadeIn / fadeInDuration;
            canvasGroupPanel.alpha = alpha;

            elapsedTimeFadeIn += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

    }



}
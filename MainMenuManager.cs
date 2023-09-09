using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject background, progressBar, login;

    // Start is called before the first frame update
    void Start()
    {
        // Execute after 1 second
        StartCoroutine(WaitAndExecute());
    }

    IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(0.1f);
        ButtonsManager.Instance.ActivateButton(1);

        yield return new WaitForSeconds(3);

        // Retrieve the CanvasGroup component from the "background" panel
        CanvasGroup canvasGroupBackground = background.GetComponent<CanvasGroup>();

        // Start a fade-out animation for the "background" panel
        float fadeOutDuration = 1.0f;
        float elapsedTimeFadeOut = 0.0f;

        while (elapsedTimeFadeOut < fadeOutDuration)
        {
            float alpha = 1 - (elapsedTimeFadeOut / fadeOutDuration);
            canvasGroupBackground.alpha = alpha;

            elapsedTimeFadeOut += Time.deltaTime;

            yield return null;
        }

        progressBar.SetActive(false);

        // Disable the "background" panel after the fade-out animation
        background.SetActive(false);

        // Enable the "login" object
        login.SetActive(true);

        // Retrieve the CanvasGroup component from the "login" object
        CanvasGroup canvasGroupLogin = login.GetComponent<CanvasGroup>();

        // Start a fade-in animation for the "login" object
        float fadeInDuration = 1.0f;
        float elapsedTimeFadeIn = 0.0f;

        while (elapsedTimeFadeIn < fadeInDuration)
        {
            float alpha = elapsedTimeFadeIn / fadeInDuration;
            canvasGroupLogin.alpha = alpha;

            elapsedTimeFadeIn += Time.deltaTime;

            yield return null;
        }
    }
}
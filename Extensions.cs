using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions
{

    public static IEnumerator FadeOut(GameObject panel)
    {
        CanvasGroup canvasGroupPanel = panel.GetComponent<CanvasGroup>();

        float fadeOutDuration = 1.0f;
        float elapsedTimeFadeOut = 0.0f;

        while (elapsedTimeFadeOut < fadeOutDuration)
        {
            float alpha = 1 - (elapsedTimeFadeOut / fadeOutDuration);
            canvasGroupPanel.alpha = alpha;

            elapsedTimeFadeOut += Time.deltaTime;

            yield return null;
        }

        canvasGroupPanel.alpha = 0.0f; // Imposta esplicitamente il valore alpha a 0.0f alla fine del tempo
    }

    public static IEnumerator FadeIn(GameObject panel)
    {
        CanvasGroup canvasGroupPanel = panel.GetComponent<CanvasGroup>();

        float fadeInDuration = 1.0f;
        float elapsedTimeFadeIn = 0.0f;

        while (elapsedTimeFadeIn < fadeInDuration)
        {
            float alpha = elapsedTimeFadeIn / fadeInDuration;
            canvasGroupPanel.alpha = alpha;

            elapsedTimeFadeIn += Time.deltaTime;

            yield return null;
        }

        canvasGroupPanel.alpha = 1.0f; // Imposta esplicitamente il valore alpha a 1.0f alla fine del tempo
    }

}
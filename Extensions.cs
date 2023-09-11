using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions
{

    public static IEnumerator FadeOut(GameObject panel)
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

    public static IEnumerator FadeIn(GameObject panel)
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
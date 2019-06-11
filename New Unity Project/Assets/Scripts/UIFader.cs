using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour
{
    public CanvasGroup uiElement;

    public void FadeIn()
    {
        StartCoroutine(fadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(fadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }

    public void FadeInAndOut()
    {
        if (uiElement.alpha == 0)
        {
            uiElement.gameObject.SetActive(true);
            FadeIn();
        }
        else
            FadeOut();
    }

    public IEnumerator fadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float porcentajeComplete = timeSinceStarted / lerpTime;

        while (true)
        {

            timeSinceStarted = Time.time - _timeStartedLerping;
            porcentajeComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, porcentajeComplete);

            cg.alpha = currentValue;

            if (porcentajeComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        if (end == 0)
        {
            uiElement.gameObject.SetActive(false);
        }
    }
}

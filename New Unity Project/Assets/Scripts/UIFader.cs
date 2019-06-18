﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aparecer y desaparecer la tabla periódica
public class UIFader : MonoBehaviour
{
    public CanvasGroup uiElement;

    public void FadeIn()
    {
        StartCoroutine(fadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }

    public void FadeOut()
    {
        //Inicia una corutina (thread) para el efecto sobre el panel
        StartCoroutine(fadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }

    /*activa el panel que en la interface esta desactivado por defecto y lanza el fade segun corresponda*/
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

    /*metodo para generar el efecto de fade in and out*/
    public IEnumerator fadeCanvasGroup(CanvasGroup cg, float start, float end)
    {
        float lerpTime = 0.5f;//tiempo de retardo en los frames.. para generar el efecto fade
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
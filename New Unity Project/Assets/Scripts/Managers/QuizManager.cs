using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public Canvas quizCanvas;
    public Canvas introCanvas;

    void Awake()
    {
        // comenzar con quiz oculto
        CanvasGroup quizCanvasGroup = quizCanvas.GetComponent<CanvasGroup>();
        quizCanvasGroup.alpha = 0;
        quizCanvasGroup.interactable = false;
    }

    public void StartQuiz()
    {
        // destruir introduccion
        Destroy(introCanvas);

        // mostrar quiz
        CanvasGroup quizCanvasGroup = quizCanvas.GetComponent<CanvasGroup>();
        quizCanvasGroup.alpha = 1;
        quizCanvasGroup.interactable = true;
    }
}

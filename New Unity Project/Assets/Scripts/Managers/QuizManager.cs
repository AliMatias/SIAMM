using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public Canvas quizCanvas;
    public Canvas introCanvas;

    private UIPopupQuestionChangeScene popupChangeScene = null;
    private QryQuiz qryQuiz;

    private List<QuestionData> allQuestions;
    private List<QuestionData> chosenQuestions;

    void Awake()
    {
        popupChangeScene = FindObjectOfType<UIPopupQuestionChangeScene>();
        // comenzar con quiz oculto
        CanvasGroup quizCanvasGroup = quizCanvas.GetComponent<CanvasGroup>();
        quizCanvasGroup.alpha = 0;
        quizCanvasGroup.interactable = false;

        GameObject go = new GameObject();
        go.AddComponent<QryQuiz>();
        qryQuiz = go.GetComponent<QryQuiz>();
        try
        {
            allQuestions = qryQuiz.GetAllQuestions();
            ChooseRandomQuestions(10);
        }
        catch (Exception e)
        {
            Debug.LogError("QuizManager :: Error getting data from database: " + e.StackTrace);

            // salir del quiz porque rompio
            popupChangeScene.MostrarErrorPopUp("Error!", "Error cargando preguntas. Se cerrará el exámen.", 1);
        }
    }

    public void StartQuiz()
    {
        // destruir introduccion
        Destroy(introCanvas.gameObject);

        // mostrar quiz
        CanvasGroup quizCanvasGroup = quizCanvas.GetComponent<CanvasGroup>();
        quizCanvasGroup.alpha = 1;
        quizCanvasGroup.interactable = true;
    }

    public void ChooseRandomQuestions(int amount)
    {
        chosenQuestions = new List<QuestionData>();
        List<QuestionData> allQuestionsTemp = new List<QuestionData>(allQuestions);
        if (amount > allQuestionsTemp.Count) amount = allQuestionsTemp.Count;
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, allQuestionsTemp.Count);
            chosenQuestions.Add(allQuestionsTemp[index]);
            allQuestionsTemp.RemoveAt(index);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public GameObject questionPrefab;
    public GameObject answerContainerPrefab;
    public GameObject inputContainerPrefab;
    public Canvas quizCanvas;
    public Canvas introCanvas;

    private UIPopupQuestionChangeScene popupChangeScene = null;
    private QryQuiz qryQuiz;

    private List<QuestionData> allQuestions;
    private List<QuestionData> chosenQuestions;

    private int currentQuestionNumber;

    void Start()
    {
        currentQuestionNumber = 0;
        popupChangeScene = FindObjectOfType<UIPopupQuestionChangeScene>();

        // comenzar con quiz oculto
        CanvasGroup quizCanvasGroup = quizCanvas.GetComponent<CanvasGroup>();
        quizCanvasGroup.alpha = 0;
        quizCanvasGroup.interactable = false;

        CanvasGroup introCanvasGroup = introCanvas.GetComponent<CanvasGroup>();
        introCanvasGroup.alpha = 1;
        introCanvasGroup.interactable = true;

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
            Debug.LogError("QuizManager :: Error getting data from database: " + e.ToString());
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
        LoadQuestion();
    }

    public void LoadQuestion()
    {
        CanvasGroup quizCanvasGroup = quizCanvas.GetComponent<CanvasGroup>();
        Transform quizTransform = quizCanvasGroup.gameObject.transform;
        QuestionData currentQuestion = allQuestions[currentQuestionNumber++];

        GameObject questionInstance = Instantiate(questionPrefab, quizTransform);
        questionInstance.GetComponent<Text>().text = currentQuestion.Question;
        if (currentQuestion.IsChoice)
        {
            GameObject answerContainer = Instantiate(answerContainerPrefab, quizTransform);
            List<AnswerData> shuffleAnswers = new List<AnswerData>(currentQuestion.Answers);

            for (int i = 0; i < answerContainer.transform.childCount; i++)
            {
                GameObject answerChoice = answerContainer.transform.GetChild(i).gameObject;

                // desordenar aleatoriamente respuestas
                int indexAnswer = Random.Range(0, shuffleAnswers.Count);
                AnswerData randomAnswer = shuffleAnswers[indexAnswer];
                answerChoice.GetComponent<Text>().text = randomAnswer.Answer;
                answerChoice.AddComponent<Button>().onClick.AddListener(
                    () =>
                    {
                        PickAnswer(randomAnswer.Id);
                        Destroy(answerContainer);
                        Destroy(questionInstance);
                        LoadQuestion();
                    }
                );
                shuffleAnswers.RemoveAt(indexAnswer);
            }
        }
        else
        {
            GameObject inputContainer = Instantiate(inputContainerPrefab, quizTransform);
        }
    }

    public void PickAnswer(int id)
    {
        Debug.Log("PICKED " + id);
    }

    public void InputAnswer(string answer)
    {
        Debug.Log("ANSWERED " + answer);
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

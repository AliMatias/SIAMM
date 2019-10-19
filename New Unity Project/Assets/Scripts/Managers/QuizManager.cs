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
    private List<AnswerQuizStruct> userAnswers;
    private List<ResultQuizStruct> quizResults;

    private int currentQuestionNumber;

    void Start()
    {
        currentQuestionNumber = 0;
        userAnswers = new List<AnswerQuizStruct>();
        quizResults = new List<ResultQuizStruct>();
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
        if (currentQuestionNumber < chosenQuestions.Count)
        {
            CanvasGroup quizCanvasGroup = quizCanvas.GetComponent<CanvasGroup>();
            Transform quizTransform = quizCanvasGroup.gameObject.transform;
            QuestionData currentQuestion = chosenQuestions[currentQuestionNumber++];

            GameObject questionInstance = Instantiate(questionPrefab, quizTransform);
            questionInstance.GetComponent<Text>().text = currentQuestionNumber + ". " + currentQuestion.Question;
            if (currentQuestion.IsChoice)
            {
                ShowMultipleChoice(quizTransform, currentQuestion, questionInstance);
            }
            else
            {
                ShowInputAnswer(quizTransform, currentQuestion, questionInstance);
            }
        }
        else
        {
            ShowResults();
        }
    }

    private void ShowInputAnswer(Transform quizTransform, QuestionData currentQuestion, GameObject questionInstance)
    {
        GameObject inputContainer = Instantiate(inputContainerPrefab, quizTransform);

        GameObject skipText = null;
        GameObject okButton = null;
        GameObject inputField = null;

        foreach (Transform child in inputContainer.transform)
        {
            switch (child.tag)
            {
                case "skipQuestion":
                    skipText = child.gameObject;
                    break;
                case "inputAnswer":
                    okButton = child.gameObject;
                    break;
                case "textAnswer":
                    inputField = child.gameObject;
                    break;
            }
        }

        skipText.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                InputAnswer("", currentQuestion);
                Destroy(inputContainer);
                Destroy(questionInstance);
                LoadQuestion();
            });

        okButton.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                InputAnswer(inputField.GetComponent<InputField>().text, currentQuestion);
                Destroy(inputContainer);
                Destroy(questionInstance);
                LoadQuestion();
            });
    }

    private void ShowMultipleChoice(Transform quizTransform, QuestionData currentQuestion, GameObject questionInstance)
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
                    PickAnswer(randomAnswer, currentQuestion);
                    Destroy(answerContainer);
                    Destroy(questionInstance);
                    LoadQuestion();
                });
            shuffleAnswers.RemoveAt(indexAnswer);
        }
    }

    public void PickAnswer(AnswerData answer, QuestionData question)
    {
        userAnswers.Add(new AnswerQuizStruct(question, answer));
    }

    public void InputAnswer(string answer, QuestionData question)
    {
        userAnswers.Add(new AnswerQuizStruct(question, answer));
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

    public void CheckResults()
    {
        userAnswers.ForEach(result =>
        {
            AnswerData correctAnswer = result.Question.Answers.Find(answer => answer.IsCorrect);
            if (result.Answer != null)
            {
                bool isCorrect = correctAnswer.Id == result.Answer.Id;
                quizResults.Add(new ResultQuizStruct(result.Question.Question, result.Answer.Answer, correctAnswer.Answer, isCorrect));
            }
            else if (result.UserInput != null)
            {
                bool isCorrect = correctAnswer.Answer.ToLower() == result.UserInput.ToLower();
                quizResults.Add(new ResultQuizStruct(result.Question.Question, result.UserInput, correctAnswer.Answer, isCorrect));
            }
        });
    }

    public void ShowResults()
    {
        CheckResults();
    }
}

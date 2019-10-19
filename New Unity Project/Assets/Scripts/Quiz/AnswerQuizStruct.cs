using UnityEngine;
using System.Collections;

public struct AnswerQuizStruct
{
    private QuestionData question;
    private AnswerData pickedAnswer;
    private string userInput;

    public QuestionData Question { get => question; }
    public AnswerData Answer { get => pickedAnswer; }
    public string UserInput { get => userInput; }

    public AnswerQuizStruct(QuestionData question, AnswerData pickedAnswer)
    {
        this.question = question;
        this.pickedAnswer = pickedAnswer;
        this.userInput = null;
    }

    public AnswerQuizStruct(QuestionData question, string userInput)
    {
        this.question = question;
        this.pickedAnswer = null;
        this.userInput = userInput;
    }
}

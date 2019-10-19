using UnityEngine;
using System.Collections;

public struct ResultQuizStruct
{
    private string question;
    private string userAnswer;
    private string correctAnswer;
    private bool isCorrect;

    public string Question { get => question; }
    public string UserAnswer { get => userAnswer; }
    public string CorrectAnswer { get => correctAnswer; }
    public bool IsCorrect { get => isCorrect; }

    public ResultQuizStruct(string question, string userAnswer, string correctAnswer, bool isCorrect)
    {
        this.question = question;
        this.userAnswer = userAnswer;
        this.correctAnswer = correctAnswer;
        this.isCorrect = isCorrect;
    }
}

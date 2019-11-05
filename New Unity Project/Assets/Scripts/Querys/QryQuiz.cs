using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QryQuiz : MonoBehaviour
{
    private DBManager dBManager = null;

    private void Awake()
    {
        dBManager = FindObjectOfType<DBManager>();
    }

    //trae un tip particular
    public List<QuestionData> GetAllQuestions()
    {
        List<QuestionData> questions = new List<QuestionData>();

        SqliteDataReader questionReader = null;
        SqliteDataReader answerReader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string questionQuery = "SELECT id, question, is_choice " +
            "FROM quiz_question;";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            questionReader = dBManager.ManageExec(dbConnection, questionQuery);

            while (questionReader.Read())
            {
                int questionId = questionReader.GetInt32(0);
                string question = dBManager.SafeGetString(questionReader, 1);
                bool isChoice = questionReader.GetBoolean(2);
                List<AnswerData> answers = new List<AnswerData>();

                string answerQuery = "SELECT id, answer, is_correct " +
            "FROM quiz_answer WHERE question_id = " + questionId + ";";

                answerReader = dBManager.ManageExec(dbConnection, answerQuery);

                while (answerReader.Read())
                {
                    int answerId = answerReader.GetInt32(0);
                    string answer = dBManager.SafeGetString(answerReader, 1);
                    bool isCorrect = answerReader.GetBoolean(2);

                    AnswerData answerData = new AnswerData(answerId, questionId, answer, isCorrect);
                    answers.Add(answerData);
                }

                QuestionData questionData = new QuestionData(questionId, question, isChoice, answers);
                questions.Add(questionData);
            }

        }
        catch (Exception e)
        {
            Debug.LogError("QryQuiz :: DB Error: " + e.ToString());
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(null, questionReader);
            dBManager.ManageClosing(dbConnection, answerReader);
        }
        return questions;
    }
}

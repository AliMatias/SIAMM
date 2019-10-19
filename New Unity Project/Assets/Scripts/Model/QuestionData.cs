// clase normal que sirve de una especie de DTO
using System;
using System.Collections.Generic;

public class QuestionData
{
    public int Id { get; set; }
    public string Question { get; set; }
    public bool IsChoice { get; set; }
    public List<AnswerData> Answers { get; set; }


    //constructor
    public QuestionData(int id, string question, bool isChoice, List<AnswerData> answers)
    {
        Id = id;
        Question = question;
        IsChoice = isChoice;
        Answers = answers;
    }

    public new string ToString => "[Pregunta" + Id + "]: " + Question;
}

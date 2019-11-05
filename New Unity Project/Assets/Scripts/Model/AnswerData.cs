// clase normal que sirve de una especie de DTO
using System;
using System.Collections.Generic;

public class AnswerData
{
    public int Id { get; set; }
    public int IdQuestion { get; set; }
    public string Answer { get; set; }
    public bool IsCorrect { get; set; }


    //constructor
    public AnswerData(int id, int idQuestion, string answer, bool isCorrect)
    {
        Id = id;
        IdQuestion = idQuestion;
        Answer = answer;
        IsCorrect = isCorrect;
    }

    public new string ToString => "[Respuesta:" + Id + "]: " + Answer + "(" + IsCorrect + ")";
}

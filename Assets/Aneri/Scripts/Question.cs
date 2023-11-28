using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public AnswerType answerType;
    public List<string> options;
    public List<int> correctAnswers; // Use indices to refer to options
}

public enum AnswerType { Radio, Checkbox, TrueFalse }

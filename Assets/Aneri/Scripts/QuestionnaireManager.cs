using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class QuestionnaireManager : MonoBehaviour
{
    public List<Question> questions;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI outputText;
    public GameObject radioPanel, checkboxPanel_4, checkboxPanel_5, trueFalsePanel; // Panels for different answer types
    public List<Toggle> radioOptions, checkboxOptions_4, checkboxOptions_5;
    public Button trueButton, falseButton;

    // refernce of the panel on the tablet.
    public GameObject processAnimPanel;
    public GameObject quizCanvas;
    public GameObject congratsPanel;

    private Question currentQuestion;
    /*private List<int> userAnswers = new List<int>();*/

    private int trueFalseAnswer = -1; // -1 indicates no selection

    void Start()
    {
        DisplayRandomQuestion();
    }

    void DisplayRandomQuestion()
    {
        if(questions.Count >= 1)
        {
            currentQuestion = questions[Random.Range(0, questions.Count)];
            questionText.text = currentQuestion.questionText;
            Debug.Log(currentQuestion.options.Count);
            // Activate the correct panel and populate options based on answerType
            // RadioButton Type.
            if (currentQuestion.answerType == AnswerType.Radio)
            {
                radioPanel.SetActive(true);

                // Set up the options inside the radioPanel.
                for (int i = 0; i < radioOptions.Count; i++)
                {
                    radioOptions[i].isOn = false;
                    radioOptions[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i];
                }
            }
            
            // Checkbox 4 options
            else if (currentQuestion.answerType == AnswerType.Checkbox && currentQuestion.options.Count.Equals(4))
            {
                Debug.Log("4 options");
                checkboxPanel_4.SetActive(true);

                // Set up the options inside the Checkbox Panel.
                for (int i = 0; i < checkboxOptions_4.Count; i++)
                {
                    checkboxOptions_4[i].isOn = false;
                    checkboxOptions_4[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i];
                }
            }
            // Checkbox 5 options
            else if (currentQuestion.answerType == AnswerType.Checkbox && currentQuestion.options.Count.Equals(5))
            {
                Debug.Log("5 options");
                checkboxPanel_5.SetActive(true);

                // Set up the options inside the Checkbox Panel.
                for (int i = 0; i < checkboxOptions_5.Count; i++)
                {
                    checkboxOptions_5[i].isOn = false;
                    checkboxOptions_5[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i];
                }
            }
            //true False
            else if (currentQuestion.answerType == AnswerType.TrueFalse)
                trueFalsePanel.SetActive(true);
        }
        
    }

    public void OnTrueButtonPressed()
    {
        trueFalseAnswer = 1; // True is selected
    }

    public void OnFalseButtonPressed()
    {
        trueFalseAnswer = 0; // False is selected
    }

    public void OnSubmit()
    {
        bool isCorrect = false; // by default false.

        if (currentQuestion.answerType == AnswerType.TrueFalse)
        {
            if (trueFalseAnswer != -1) // Ensure the user has made a selection
            {
                isCorrect = (currentQuestion.correctAnswers.Contains(trueFalseAnswer));
            }
            else
            {
                Debug.Log("Please make a selection");
                outputText.text = "Please make a selection";
            }
        }
        else
        {
            isCorrect = CheckAnswer();
        }

        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            outputText.text = "Correct Answer" + " \n Feedback for the same";
        }  
        else
        {
            Debug.Log("Incorrect Answer");
            outputText.text = "Incorrect Answer"  + " \n Feedback for the same";
        }
            

        // removing and disappearing the currentQuestion and its corrsponding answer panel.
        questions.Remove(currentQuestion);
        questionText.text = "";
        if (currentQuestion.answerType == AnswerType.Radio)
        {
            // Set up the options inside the radioPanel.
            for (int i = 0; i < radioOptions.Count; i++)
            {
                radioOptions[i].isOn = false;
            }

            radioPanel.SetActive(false);
        }

        else if (currentQuestion.answerType == AnswerType.Checkbox && currentQuestion.options.Count == 4)
        {
            // Set up the options inside the Checkbox Panel.
            for (int i = 0; i < checkboxOptions_4.Count; i++)
            {
                checkboxOptions_4[i].isOn = false;
            }


            checkboxPanel_4.SetActive(false);
        }

        else if (currentQuestion.answerType == AnswerType.Checkbox && currentQuestion.options.Count == 5)
        {
            // Set up the options inside the Checkbox Panel.
            for (int i = 0; i < checkboxOptions_5.Count; i++)
            {
                checkboxOptions_5[i].isOn = false;
            }


            checkboxPanel_5.SetActive(false);
        }

        else if (currentQuestion.answerType == AnswerType.TrueFalse)
            trueFalsePanel.SetActive(false);

        // if there are no questions then close the congrats panel, quiz canvas and the tablet process panel.
        if(questions.Count == 0)
        {
            congratsPanel.SetActive(false);
            quizCanvas.SetActive(false);
            processAnimPanel.SetActive(false);
        }
        else
        {
            DisplayRandomQuestion(); // Display another question
        }
        
    }

    bool CheckAnswer()
    {
        // Logic to check if user's answer matches the correct answer
        
        // RadioButton Type answer.
        if (currentQuestion.answerType == AnswerType.Radio)
        {
            for(int i = 0; i < radioOptions.Count; i++)
            {
                if(radioOptions[i].isOn && i == currentQuestion.correctAnswers[0])
                {
                    return true; // correct answer.
                }
            }
            return false; // Incorrect answer.
        }

        // Checkbox Type answer.
        if(currentQuestion.answerType == AnswerType.Checkbox && currentQuestion.options.Count == 5)
        {
            List<int> SelectedAnswers = new List<int>(); // stores the options selected by the user.
            for(int i = 0; i < checkboxOptions_5.Count; i++)
            {
                if (checkboxOptions_5[i].isOn)
                    SelectedAnswers.Add(i);
            }

            // now we will use Except() of the list to give us any different values amongst the both list, if it doesn't give any
            // different value, we will know that the selectedAnswers and the correctAnswers has same values.
            // Also we need to match the count in both lists.
            return SelectedAnswers.Count == currentQuestion.correctAnswers.Count &&
                   !SelectedAnswers.Except(currentQuestion.correctAnswers).Any();
        }

        // Checkbox Type answer.
        if (currentQuestion.answerType == AnswerType.Checkbox && currentQuestion.options.Count == 4)
        {
            List<int> SelectedAnswers = new List<int>(); // stores the options selected by the user.
            for (int i = 0; i < checkboxOptions_4.Count; i++)
            {
                if (checkboxOptions_4[i].isOn)
                    SelectedAnswers.Add(i);
            }

            // now we will use Except() of the list to give us any different values amongst the both list, if it doesn't give any
            // different value, we will know that the selectedAnswers and the correctAnswers has same values.
            // Also we need to match the count in both lists.
            return SelectedAnswers.Count == currentQuestion.correctAnswers.Count &&
                   !SelectedAnswers.Except(currentQuestion.correctAnswers).Any();
        }

        return false; // or false based on the checking
    }

    // Additional methods to handle user interactions
    // ...
}

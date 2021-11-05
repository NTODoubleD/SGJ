using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class QuizContoller : MonoBehaviour
{

    [SerializeField] private Text QuestionText;

    [SerializeField] private GameObject Button1;
    [SerializeField] private GameObject Button2;
    [SerializeField] private GameObject Button3;
    [SerializeField] private GameObject Button4;

    [SerializeField] private QuizParameters _parameters;


    [HideInInspector] public QuizContoller Instance;
    private int _currentQuizID = 0;
    private int _answer;

    private Text Button1Text;
    private Text Button2Text;
    private Text Button3Text;
    private Text Button4Text;

    private void Awake()
    {
        Instance = this;
        Button1Text = Button1.GetComponentInChildren<Text>();
        Button2Text = Button2.GetComponentInChildren<Text>();
        Button3Text = Button3.GetComponentInChildren<Text>();
        Button4Text = Button4.GetComponentInChildren<Text>();
        SetQuiz();
    }

    private void SetQuiz()
    {
        var currentQuiz = _parameters.AllQuizParameters[_currentQuizID];
        QuestionText.text = currentQuiz.Question;
        _answer = currentQuiz.AnswerButtonID;
        Button1Text.text = currentQuiz.Button1;
        Button2Text.text = currentQuiz.Button2;
        Button3Text.text = currentQuiz.Button3;
        Button4Text.text = currentQuiz.Button4;
    }

    private void SetNewQuiz()
    {
        _currentQuizID++;
        if (_currentQuizID < _parameters.AllQuizParameters.Length)
            SetQuiz();
        else
            print("You won!");
    }

    public void TryToAnswer(int ID)
    {
        if (ID == _answer)
            SetNewQuiz();
    }



}

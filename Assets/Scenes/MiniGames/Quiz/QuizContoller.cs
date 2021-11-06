using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;   

public class QuizContoller : MonoBehaviour
{

    public UnityEvent OnLose, OnWin;

    [SerializeField] private Text QuestionText;

    [SerializeField] private PartOfQuize[] parts;

    [SerializeField] private QuizParameters _parameters;


    [HideInInspector] public QuizContoller Instance;
    private int _currentQuizID = 0;
    private int _answer;

    private bool isTimer;

    private void Awake()
    {
        Instance = this;
        foreach (var item in parts)
        {
            item.text = item.button.GetComponentInChildren<Text>();
        }
        SetQuiz();
    }

    private void SetQuiz()
    {
        var currentQuiz = _parameters.AllQuizParameters[_currentQuizID];
        QuestionText.text = currentQuiz.Question;
        _answer = currentQuiz.AnswerButtonID;
        foreach (var item in parts)
        {
            item.text = item.button.GetComponentInChildren<Text>();
        }

        for (int i = 0; i < 4; i++)
        {
            parts[i].text.text = currentQuiz.buttons[i];
        }
    }

    private void SetColors()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if(i == _answer - 1)
                parts[i].image.color = Color.green;
            else
                parts[i].image.color = Color.red;
            parts[i].image.gameObject.SetActive(true);
        }
        
    }

    private void DisableAll()
    {
        foreach (var item in parts)
        {
            item.image.gameObject.SetActive(false);
        }
    }

    private void SetNewQuiz()
    {
        _currentQuizID++;
        if (_currentQuizID < _parameters.AllQuizParameters.Length)
            SetQuiz();
        else
            CloseQuize();
    }

    public void TryToAnswer(int ID)
    {
        SetColors();
        StartCoroutine(ITimer(ID));
    }
    

    private IEnumerator ITimer(int ID)
    {
        if (!isTimer)
        {
            isTimer = true;
            yield return new WaitForSeconds(2f);
            isTimer = false;
            DisableAll();
            if (ID == _answer)
                SetNewQuiz();
            else
                CloseQuize();
        }

    }
    
    [System.Serializable]
    public class PartOfQuize
    {
        public GameObject button;
        [HideInInspector] public Text text;
        public Image image;
    }

    public void Win()
    {
        OnWin.Invoke();
        CloseQuize();
    }

    public void Lose()
    {
        OnLose.Invoke();
        CloseQuize();
    }

    public void CloseQuize()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

}

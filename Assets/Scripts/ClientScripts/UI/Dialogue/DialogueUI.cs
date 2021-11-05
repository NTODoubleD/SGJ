using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum DialogueMood
{
    Idle,
    Agressive
}

[System.Serializable]
public struct DialogueParameters
{
    public string[] DialougeTexts;

    public string ButtonText1;
    public string ButtonText2;

    public UnityEvent ActionOnButton1;
    public UnityEvent ActionOnButton2;
    public UnityEvent ActionOnEnd;
    public AudioClip[] Audios;
    public DialogueMood Mood;
}


public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Text _dialougeText;
    [SerializeField] private int _readSpeed;
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    [SerializeField] private Button _skipButton;

    [SerializeField] private GameObject _mediator;
    [SerializeField] private GameObject _dialogueScreen;
    [SerializeField] private GameObject _chooseScreen;

    private string _text;

    public static DialogueUI Instance;

    private int _currentTextID = 0;

    private void Awake()
    {
        Instance = this;
        _mediator.SetActive(false);
    }

    private void OnEnable()
    {
        _skipButton.onClick.AddListener(SetNewTalk);
    }

    private void OnDisable()
    {
        _skipButton.onClick.RemoveListener(SetNewTalk);
    }

    private DialogueParameters _parameters;

    public void SetNewDialouge(DialogueParameters parameters)
    {
        _mediator.SetActive(true);
        _dialogueScreen.SetActive(true);
        _chooseScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        _parameters = parameters;

        _currentTextID = -1;
        SetNewTalk();
    }

    private void SetNewTalk()
    {
        _currentTextID++;
        if (_currentTextID >= _parameters.DialougeTexts.Length)
        {
            SetChooseScreen();
            return;
        }

        _text = _parameters.DialougeTexts[_currentTextID];
        _dialougeText.text = "";
        StartCoroutine(ReadText());
    }


    private void SetChooseScreen()
    {
        _dialogueScreen.SetActive(false);
        _chooseScreen.SetActive(true);
        _button1.onClick.AddListener(InvokeButton1);
        _button2.onClick.AddListener(InvokeButton2);
        _button1.GetComponentInChildren<Text>().text = _parameters.ButtonText1;
        _button2.GetComponentInChildren<Text>().text = _parameters.ButtonText2;
    }

    private void InvokeButton1()
    {
        _parameters.ActionOnButton1?.Invoke();
        DisableMediator();
    }

    private void InvokeButton2()
    {
        _parameters.ActionOnButton2?.Invoke();
        DisableMediator();
    }

    private void DisableMediator()
    {
        _button1.onClick.RemoveListener(InvokeButton1);
        _button2.onClick.RemoveListener(InvokeButton2);
        _dialogueScreen.SetActive(false);
        _chooseScreen.SetActive(false);
        _mediator.SetActive(false);
        _parameters.ActionOnEnd?.Invoke();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private System.Collections.IEnumerator ReadText()
    {
        var builder = new StringBuilder();
        foreach (var item in _text)
        {
            yield return new WaitForSeconds(1 / _readSpeed);
            builder.Append(item);
            _dialougeText.text = builder.ToString();
        }
    }


    

}

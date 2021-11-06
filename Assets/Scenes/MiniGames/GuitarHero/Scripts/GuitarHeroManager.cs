using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

[System.Serializable]
public struct GuitarHeroParamaters
{
    public GuitarHeroParameter[] Parameters;
}

[System.Serializable]
public class GuitarHeroParameter
{
    public bool Up, Down, Left, Right = false;
}


public class GuitarHeroManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnUp, _spawnDown, _spawnRight, _spawnLeft;
    [SerializeField] private GameObject _piecePrefab;
    [SerializeField] private GuitarHeroButton _buttonUp, _buttonDown, _buttonLeft, _buttonRight;

    [SerializeField] private float _spawnDelay;
    [SerializeField] private TextAsset _textAsset;

    [SerializeField] private AnimationCurve _accleration;

    public static Action OnLifeLost;
    private int _lifes;

    private float _currentTime = 0;

    private List<bool[]> _parameters;

    private List<string> _textParameters = new List<string>();

    private void Start()
    {
        ReadINI();
        StartGame();
    }

    private void StartGame()
    {
        _lifes = 3;
        StartCoroutine(SpawnNewPieces());
        
    }
    private void OnEnable()
    {
        OnLifeLost += LostLife;
    }
    private void OnDisable()
    {
        OnLifeLost -= LostLife;
    }

    private void LostLife()
    {
        _lifes--;
        if (_lifes <= 0)
        {
            print("You lost");
        }
    }

    private void SpawnNewPiece(Vector3 where, GHButtonType type)
    {
        var newPiece = Instantiate(_piecePrefab, where, Quaternion.identity);
        newPiece.transform.parent = transform;
        var newBehaviour = newPiece.GetComponent<GuiratHeroPiece>();
        newBehaviour.SetType(type);

        newBehaviour.speed = _accleration.Evaluate(_currentTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
            _buttonUp.TryDestroyCurrentObject();
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            _buttonDown.TryDestroyCurrentObject();
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
            _buttonLeft.TryDestroyCurrentObject();
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.RightArrow))
            _buttonRight.TryDestroyCurrentObject();
    }

    private IEnumerator SpawnNewPieces()
    {
        var i = 0;
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);
            _currentTime += _spawnDelay;
            var newParameters = _parameters[i];

            if (newParameters[0])
                SpawnNewPiece(_spawnUp.position, GHButtonType.Up);
            if (newParameters[1])
                SpawnNewPiece(_spawnDown.position, GHButtonType.Dowm);
            if (newParameters[2])
                SpawnNewPiece(_spawnRight.position, GHButtonType.Right);
            if (newParameters[3])
                SpawnNewPiece(_spawnLeft.position, GHButtonType.Left);
            i++;

            if (i > _parameters.Count)
                break;
        }
        
    }


    private void ReadINI()
    {
        var stringReader = new StringReader(_textAsset.ToString());

        while (true)
        {
            var newLine = stringReader.ReadLine();
            if (newLine == "End")
                break;

            _textParameters.Add(newLine);
        }

        int i = 0;
        int k = 0;
        int position = 0;

        _parameters = new List<bool[]>();

        foreach (var item in _textParameters)
        {
            while (true)
            {
                _parameters.Add(new bool[4]);

                if (item[i] == '0')
                {
                    _parameters[k][position] = false;
                    position++;
                }
                else if (item[i] == '1')
                {
                    _parameters[k][position] = true;
                    position++;
                }
                if (position > 3)
                {
                    k++;
                    i = 0;
                    position = 0;
                    break;
                }
                i++;


            }

           // print(item[0] + " " + item[2] + " " + item[4] + " " + item[6]);
           // print(_parameters[k-1][0] + " " + _parameters[k-1][1] + " " + _parameters[k-1][2] + " " + _parameters[k-1][3]);

        }
    }
}


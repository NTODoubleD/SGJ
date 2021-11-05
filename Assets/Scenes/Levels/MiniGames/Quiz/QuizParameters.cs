﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct QuizParameters
{
    public SpecialQuizParameters[] AllQuizParameters;
}

[System.Serializable]
[SelectionBase]
public class SpecialQuizParameters
{
    public string Question;
    [Space]
    [Tooltip("An integer using the Range attribute")]
    [Range(1, 4)]
    [SerializeField]
    public int AnswerButtonID;

    public string Button1;
    public string Button2;
    public string Button3;
    public string Button4;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudioBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip _battleMusic;

    private void Start()
    {
        ImperialClass.Instance.OnStateChange += StartBattleMusic;
    }

    private void StartBattleMusic()
    {
        if (ImperialClass.Instance.State == ImperialStates.HuntingPlayer)
        {
            AudioBehaviour.Instance.PlayMusic(_battleMusic);
        }
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TeamMusicParameters
{
    public int Team;
    public AudioClip Clip;
}

public class LevelAudioBehaviour : MonoBehaviour
{
    [SerializeField] private TeamMusicParameters[] _parameters;
    private int _currentHuntingTeam;


    private void Start()
    {
        ImperialClass.Instance.OnHuntingPlayer += StartTeamBattleMusic;
        Enemy.OnTeamDead += StopTeamBattleMusic;
    }


    private void StartTeamBattleMusic(int team)
    {
        foreach(var item in _parameters)
        {
            if (item.Team == team)
            {
                AudioBehaviour.Instance.PlayMusic(item.Clip);
                _currentHuntingTeam = team;
            }
                
        }
    }

    private void StopTeamBattleMusic(int team)
    {
        if (team == _currentHuntingTeam)
            AudioBehaviour.Instance.StopMusic();
    }

}

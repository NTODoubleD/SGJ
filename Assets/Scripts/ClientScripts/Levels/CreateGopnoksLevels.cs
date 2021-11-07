using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGopnoksLevels : MonoBehaviour
{
    [SerializeField] private GameObject _arena;
    [SerializeField] private int _team;
    private void Start()
    {
        _arena.SetActive(false);
        Enemy.OnTeamDead += OffArena;
    }

    public void SetArena()
    {
        _arena.SetActive(true);
    }

    public void OffArena(int team)
    {
        if (team == _team)
        {
            _arena.SetActive(false);
            AudioBehaviour.Instance.StopMusic();
            PlayerBehaviour.Instance._damageSystem.Heal();
        }
    }
            

}

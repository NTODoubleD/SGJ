using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponAudioClip
{
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public void SetAudio()
    {
        AudioSource.clip = AudioClip;
    }

    public void PlayAudio(float pitch = 0, bool checkFinished = false)
    {
        if (checkFinished)
        {
            if (AudioSource.isPlaying)
                return;
        }
        if (pitch == 0)
            AudioSource.pitch = Random.Range(0.8f, 1.2f);
        else
            AudioSource.pitch = pitch;
        AudioSource.Play();
    }
    
    public void StopAudio()
    {
        AudioSource.Stop();
    }

}

public class Bulava : Sword
{
    [SerializeField] private WeaponAudioClip _attackClip, _getClip, _hit;

    protected override void Start()
    {
        base.Start();
        onAttack.AddListener(PlayAttack);
        _attackClip.SetAudio();
        _getClip.SetAudio();
        _hit.SetAudio();
    }

    protected void PlayAttack()
    {
        _attackClip.PlayAudio();
    }

    protected override void GiveDamage(Collider other)
    {
        base.GiveDamage(other);
        _hit.PlayAudio(0.5f);
    }

    public override void ChangeExsist(bool exsist)
    {
        base.ChangeExsist(exsist);
        if (exsist)
        {
            _getClip.PlayAudio();
        }
        else
        {
            _attackClip.StopAudio();
        }
    }
}

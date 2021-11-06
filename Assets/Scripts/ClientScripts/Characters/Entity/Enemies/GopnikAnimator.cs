using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopnikAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementSpeed = 1f;


    private void Awake()
    {
        _animator.SetBool("IsTalking", false);
        _animator.SetFloat("Speed", 0f);
    }

    public void SetDialogueAnimation(DialogueMood mood)
    {
        _animator.SetBool("IsTalking", true);
        if (mood == DialogueMood.Idle)
        {
            _animator.SetBool("IsAngryTalk", false);
        }
        else
        {
            _animator.SetBool("IsAngryTalk", true);
        }
    }

    public void PrepareHuntAnimation()
    {
        _animator.SetBool("IsTalking", false);
        _animator.SetTrigger("GetAngry");
    }

    public void SetHuntAnimation()
    {
        _animator.SetFloat("Speed", _movementSpeed);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void GetDamage()
    {
        _animator.SetInteger("TakeValue", Random.Range(0, 3));
        _animator.SetTrigger("TakeDamage");
    }
}

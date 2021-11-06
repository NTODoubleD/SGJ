using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GopnikAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private float _speed = 1.25f;

    private float CalculatedSpeed() { return Mathf.Sqrt(Mathf.Pow(_navMesh.velocity.x, 2) + Mathf.Pow(_navMesh.velocity.y, 2) + Mathf.Pow(_navMesh.velocity.z, 2)); }


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

    private void Update()
    {
        _animator.SetFloat("Speed", CalculatedSpeed() / _speed);
    }

    public void Attack()
    {
        _animator.SetInteger("AttackValue", Random.Range(0, 1));
        _animator.SetTrigger("Attack");
    }

    public void GetDamage()
    {
        _animator.SetInteger("TakeValue", Random.Range(0, 3));
        _animator.SetTrigger("TakeDamage");
    }
}

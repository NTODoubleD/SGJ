using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollBehaviour : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;

    [SerializeField] private HealthBar _component0;
    [SerializeField] private DamageSystem _component1;
    [SerializeField] private Enemy _component2;
    [SerializeField] private Weapon _component3;
    [SerializeField] private DialogueCharacter _component4;
    [SerializeField] private Canvas _component5;
    [SerializeField] private Rigidbody _component6;
    [SerializeField] private NavMeshAgent _component7;
    [SerializeField] private CapsuleCollider _component8;
    [SerializeField] private GameObject _mainObject;


    private bool _isDying = false;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();
        foreach (var item in _rigidbodies)
        {
            item.useGravity = false;
        }
        foreach (var item in _colliders)
        {
            if (item.isTrigger is false)
                item.enabled = false;
        }
    }

    public void Kill()
    {
        _component0.ChangeToHealth(0f);
        _component1.enabled = false;
        _component2.enabled = false;
        _component3.ChangeExsist(false);
        _component4.enabled = false;
        _component5.enabled = false;
        _component6.isKinematic = true;
        _component7.enabled = false;
        _component8.enabled = false;
        _animator.enabled = false;


        foreach (var item in _rigidbodies)
        {
            item.useGravity = true;
        }
        foreach (var item in _colliders)
        {
            item.enabled = true;
        }

        
        StartCoroutine(DisableColliders());
        StartCoroutine(StartDying());
        Destroy(_mainObject, 35f);
    }

    private void Update()
    {
        if (_isDying)
            _mainObject.transform.position += (Vector3.down * Time.deltaTime)/15f;
    }

    private IEnumerator DisableColliders()
    {
        yield return new WaitForSeconds(10f);
        foreach (var item in _rigidbodies)
        {
            item.useGravity = false;
        }
        foreach (var item in _colliders)
        {
            if (item.isTrigger is false)
                item.enabled = false;
        }
    }

    private IEnumerator StartDying()
    {
        yield return new WaitForSeconds(15f);
        _isDying = true;

    }
}

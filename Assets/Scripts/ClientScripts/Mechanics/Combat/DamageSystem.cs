using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DamageSystem : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _knockbackAmount;
    [SerializeField] private GameObject _particleOnHit;
    [SerializeField] private bool _postProcessReact = false;
    [SerializeField] private RagdollBehaviour _ragdole;
    [SerializeField] private Transform _bloodSpawn;

    public bool DestroyOnDead = true;

    private int _maxHealth;

    private Rigidbody _rigidbody;
    private Enemy _enemy;

    public Action<int, int> OnHealthChanged;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemy = GetComponent<Enemy>();

        _maxHealth = _health;
    }


    public void GetDamage(int damage)
    {
        _health -= damage;

        if (_bloodSpawn == null)
            Instantiate(_particleOnHit, transform.position, Quaternion.identity);
        else
            Instantiate(_particleOnHit, _bloodSpawn.position, Quaternion.identity);

        OnHealthChanged?.Invoke(_health, _maxHealth);
        CheckState();

        _enemy?.Damage();

        if (_postProcessReact)
        {
            PostProcessingBehaviour.Instance.FillRedVignette();
        }
    }


    private void CheckState()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (DestroyOnDead is false)
        {
            print("You're dead");
            return;
        }

        _enemy?.Die();
        if (_ragdole is null)
            Destroy(gameObject);
        else
            _ragdole?.Kill();
    }

    public void SetKnockback(Vector3 otherPositon, float knockbackAmount)
    {
        _enemy?.StopAgentByTime(0.25f);

        var newKnockback = - (otherPositon - transform.position).normalized;
        newKnockback.y = 0;
        newKnockback = newKnockback.normalized;
        _rigidbody?.AddForce(newKnockback * _knockbackAmount, ForceMode.Impulse);
    }

}

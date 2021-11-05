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

    private int _maxHealth;

    private Rigidbody _rigidbody;

    public Action<int, int> OnHealthChanged;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _maxHealth = _health;
    }


    public void GetDamage(int damage)
    {
        _health -= damage;

        Instantiate(_particleOnHit, transform.position, Quaternion.identity);

        OnHealthChanged?.Invoke(_health, _maxHealth);
        CheckState();
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
        Destroy(gameObject);
    }

    public void SetKnockback(Vector3 otherPositon, float knockbackAmount)
    {
        var newKnockback = - (otherPositon - transform.position).normalized;
        newKnockback.y = 0;
        newKnockback = newKnockback.normalized;
        _rigidbody.AddForce(newKnockback * _knockbackAmount, ForceMode.Impulse);
    }
}

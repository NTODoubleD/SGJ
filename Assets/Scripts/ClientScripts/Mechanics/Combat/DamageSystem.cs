using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DamageSystem : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _knockbackAmount;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    public void GetDamage(int damage)
    {
        _health -= damage;

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
        var newKnockback = - (otherPositon - transform.position);
        newKnockback.y = 0;

        _rigidbody.AddForce(newKnockback * _knockbackAmount, ForceMode.Impulse);
    }
}

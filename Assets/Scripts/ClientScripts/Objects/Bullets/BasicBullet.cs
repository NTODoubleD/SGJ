using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        GiveDamage(other);
    }

    private void GiveDamage(Collider other)
    {

        DamageSystem otherDamageSystem;
        if (other.gameObject.TryGetComponent<DamageSystem>(out otherDamageSystem))
        {
            otherDamageSystem?.GetDamage(_damage);
            otherDamageSystem?.SetKnockback(transform.position, 0);
        }
        Destroy(gameObject);

    }
}

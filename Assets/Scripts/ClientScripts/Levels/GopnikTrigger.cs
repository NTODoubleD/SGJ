using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopnikTrigger : MonoBehaviour
{
    [SerializeField] private DialogueCharacter _mainGopnik;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            _mainGopnik.Interact();
            Destroy(gameObject);
        }

    }
}

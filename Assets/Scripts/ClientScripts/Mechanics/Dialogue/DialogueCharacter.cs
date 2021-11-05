﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueCamera;

    [SerializeField] private DialogueParameters _dialougeParameters;

    private void Awake()
    {
        _dialogueCamera.SetActive(false);
    }

    private void Start()
    {
        PlayerBehaviour.Instance.OnRaycast += CheckRaycast;
        
    }

    private void CheckRaycast(RaycastHit hit)
    {
        if (hit.transform?.gameObject == gameObject)
        {
            TryInteract();
        }
    }

    private void OnDestroy()
    {
        if (PlayerBehaviour.Instance != null)
            PlayerBehaviour.Instance.OnRaycast -= CheckRaycast;
    }


    private void TryInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        ImperialClass.Instance.SetState(ImperialStates.Dialogue);
        PlayerBehaviour.Instance.PlayerCamera.gameObject.SetActive(false);
        _dialogueCamera.SetActive(true);

        DialogueUI.Instance.SetNewDialouge(_dialougeParameters);
    }


    public void SetCamera()
    {
        PlayerBehaviour.Instance.PlayerCamera.gameObject.SetActive(true);
        _dialogueCamera.SetActive(false);
        ImperialClass.Instance.SetState(ImperialStates.PlayerMove);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string[] _dialogue;

    private DialogueManager _dialogueManager;
    public override void Interact(PlayerController player)
    {
        //Debug.Log($"El NPC {_name} interactuo");
        if (_dialogueManager != null)
        {
            _dialogueManager.setDialogue(_name, _dialogue);
            //Enviar la información del NPC al dialoguemanager
        }
    }

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        if (_dialogueManager == null)
        {
            Debug.LogError("La escena no tiene un manejador de dialogos");
        }
       
    }
}

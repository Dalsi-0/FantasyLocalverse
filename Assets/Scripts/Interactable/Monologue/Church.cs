using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : InteractableBase
{
    [SerializeField] private DialogueData[] dialogueData;

    private void Start()
    {
        dialogueData = DialogueManager.Instance.repository.GetDialogue("Object_church");

        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData);
    }


}

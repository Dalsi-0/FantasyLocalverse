using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : InteractableBase
{
    private DialogueData[] dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        dialogueData = DialogueManager.Instance.repository.GetDialogue("Object_church");
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => SetVirtualCameraActive(false));
    }

}

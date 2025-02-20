using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueDataSO;

public class Church : InteractableBase
{
    private List<DialogueLine> dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        dialogueData = DialogueManager.Instance.GetDialogue(EDialogueKey.Object_church);
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => SetVirtualCameraActive(false));
    }

}

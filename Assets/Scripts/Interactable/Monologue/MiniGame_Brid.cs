using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Block : InteractableBase
{
    private DialogueData[] dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        dialogueData = DialogueManager.Instance.repository.GetDialogue("MiniGame_Block");
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => SetVirtualCameraActive(false));
    }


}

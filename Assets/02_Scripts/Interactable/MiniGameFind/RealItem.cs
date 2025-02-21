using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueDataSO;

public class RealItem : InteractableBase
{
    private List<DialogueLine> dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        dialogueData = DialogueManager.Instance.GetDialogue(EDialogueKey.MiniGame_RealItem);

        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () =>
        {
            FindObjectOfType<MiniGameFindManager>().EndGame();
        });
    }

}

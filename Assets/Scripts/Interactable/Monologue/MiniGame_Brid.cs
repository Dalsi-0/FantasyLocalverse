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
        dialogueData = DialogueManager.Instance.repository.GetDialogue("MiniGame_Brid");
        //onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => SetVirtualCameraActive(false));
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => SceneLoader.Instance.LoadScene(ESceneType.MiniGameBrid));
    }


}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueDataSO;

public class MiniGame_Brid : InteractableBase
{
    private List<DialogueLine> dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        dialogueData = DialogueManager.Instance.repository.GetDialogue(EDialogueKey.MiniGame_Brid);
        //onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => SetVirtualCameraActive(false));
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => activeQuestUI());
    }

    void activeQuestUI()
    {
        // ui에 정보 전달
        UIManager.Instance.UpdateMiniGameUI(ESceneType.MiniGameBrid);
        // 레터박스 비활성화



        // 화면 잠금 및 움직임 잠금 다시 설정하기 

    }
}

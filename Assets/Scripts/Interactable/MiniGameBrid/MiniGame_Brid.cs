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
        // ui�� ���� ����
        UIManager.Instance.UpdateMiniGameUI(ESceneType.MiniGameBrid);
        // ���͹ڽ� ��Ȱ��ȭ



        // ȭ�� ��� �� ������ ��� �ٽ� �����ϱ� 

    }
}

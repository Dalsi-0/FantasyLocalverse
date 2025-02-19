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

    void sefsef()
    {
        // 다이얼로그는 끄고 UI 활성화
        // UI에 정보 전달하기

    }
}

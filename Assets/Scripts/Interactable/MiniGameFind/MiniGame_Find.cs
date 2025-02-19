using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueDataSO;

public class MiniGame_Find : InteractableBase
{
    private List<DialogueLine> dialogueData;

    private void Start()
    {
        StartCoroutine(UnlockInputAfterDelay());

        SetVirtualCameraActive(false);
        dialogueData = DialogueManager.Instance.repository.GetDialogue(EDialogueKey.MiniGame_Find);
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => ActiveMiniGameUI());
    }

    void ActiveMiniGameUI()
    {
        SetVirtualCameraActive(false);
        UIManager.Instance.UpdateMiniGameUI(ESceneType.MiniGameFind);
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        int delay = 0;
        while (true)
        {
            delay = Random.Range(3, 9);
            yield return new WaitForSeconds(delay);
            BubbleManager.Instance.ShowBubble(gameObject, "아이고..\n내 물건이 어디갔나..?");
            yield return new WaitForSeconds(5f);
        }
    }
}

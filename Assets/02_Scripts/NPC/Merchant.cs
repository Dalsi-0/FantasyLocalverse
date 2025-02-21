using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueDataSO;

public class Merchant : InteractableBase
{
    private List<DialogueLine> dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        StartCoroutine(UnlockInputAfterDelay());

        dialogueData = DialogueManager.Instance.GetDialogue(EDialogueKey.NPC_merchant);
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, () => SetVirtualCameraActive(false));
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        int delay = 0;
        while (true)
        {
            delay = Random.Range(3, 6);
            yield return new WaitForSeconds(delay);
            BubbleManager.Instance.ShowBubble(gameObject, "���� �ϳ� �簡����~");
            yield return new WaitForSeconds(5f);
        }
    }
}

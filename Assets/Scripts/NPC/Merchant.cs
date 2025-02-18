using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : InteractableBase
{
    private DialogueData[] dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        StartCoroutine(UnlockInputAfterDelay());

        dialogueData = DialogueManager.Instance.repository.GetDialogue("NPC_merchant");
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData);
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        int delay = 0;
        while (true)
        {
            delay = Random.Range(3, 6);
            yield return new WaitForSeconds(delay);
            BubbleManager.Instance.ShowBubble(gameObject, "과일 하나 사가세요~");
            yield return new WaitForSeconds(5f);
        }
    }
}

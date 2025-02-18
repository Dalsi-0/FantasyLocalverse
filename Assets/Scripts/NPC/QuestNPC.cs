using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : InteractableBase
{
    private DialogueData[] dialogueData;

    private void Start()
    {
        StartCoroutine(UnlockInputAfterDelay());

        dialogueData = DialogueManager.Instance.repository.GetDialogue("NPC_merchant");
        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera);
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        int delay = 0;
        while (true)
        {
            delay = Random.Range(3, 9);
            yield return new WaitForSeconds(delay);
            BubbleManager.Instance.ShowBubble(gameObject, "���̰�..\n�� ������ ��𰬳�..?");
            yield return new WaitForSeconds(5f);
        }
    }
}

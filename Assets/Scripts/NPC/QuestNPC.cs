using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour, IInteractable
{
    private bool isFirstTime;

    private void Start()
    {
        StartCoroutine(UnlockInputAfterDelay());
    }

    public void Interact()
    {
        isFirstTime = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFirstTime && collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}

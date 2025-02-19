using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard2 : InteractableBase
{
    private void Start()
    {
        ActiveCoroutine();
    }

    void ActiveCoroutine()
    {
        StartCoroutine(UnlockInputAfterDelay());
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        int delay = 0;
        while (true)
        {
            delay = Random.Range(3, 9);
            yield return new WaitForSeconds(delay);
            BubbleManager.Instance.ShowBubble(gameObject, "근무 교대할때 불러라.");
            yield return new WaitForSeconds(5f);
        }
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();

            BubbleManager.Instance.ShowBubble(gameObject, "당신 뭐야? 저리 가라");
            Invoke("ActiveCoroutine", 5f);
        }
    }
}

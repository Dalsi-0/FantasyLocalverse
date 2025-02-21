using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard2 : InteractableBase
{
    public GameObject hitText;

    private void Start()
    {
        ActiveCoroutine();
    }

    private void ActiveCoroutine()
    {
        hitText.SetActive(true);
        StartCoroutine(UnlockInputAfterDelay());
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        int delay = 0;
        while (true)
        {
            delay = Random.Range(3, 9);
            yield return new WaitForSeconds(delay);
            BubbleManager.Instance.ShowBubble(gameObject, "�ٹ� �����Ҷ� �ҷ���.");
            yield return new WaitForSeconds(5f);
        }
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hitText.SetActive(false);
            StopAllCoroutines();

            BubbleManager.Instance.ShowBubble(gameObject, "��� ����? ���� ����");
            Invoke("ActiveCoroutine", 5f);
        }
    }
}

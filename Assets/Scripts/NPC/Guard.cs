using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour, IInteractable
{
    private bool isFirstTime;

    public void Interact()
    {
        isFirstTime = true;
        BubbleManager.Instance.ShowBubble(gameObject, "안녕하세요.\n로컬버스 마을입니다.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFirstTime && collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public Action onInteract; // ������ ������ �����ϴ� ��������Ʈ

    public void Interact()
    {
        onInteract?.Invoke(); // �Ҵ�� ��� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().SetInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ClearInteractable();
        }
    }
}

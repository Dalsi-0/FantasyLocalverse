using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public Action onInteract; // ������ ������ �����ϴ� ��������Ʈ
    [SerializeField] protected GameObject virtualCamera;

    public void Interact()
    {
        onInteract?.Invoke(); // �Ҵ�� ��� ����
    }


    protected void SetVirtualCameraActive(bool isActive)
    {
        virtualCamera.SetActive(isActive);

        if (!isActive)
        {
            Camera.main.orthographicSize = 3;
        }
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public Action onInteract; // ������ ������ �����ϴ� ��������Ʈ
    [SerializeField] protected GameObject virtualCamera;

    /// <summary>
    /// ��ȣ�ۿ��� �����ϴ� �Լ�
    /// </summary>
    public void Interact()
    {
        onInteract?.Invoke();
    }

    /// <summary>
    /// ���� ī�޶� Ȱ��ȭ ��Ȱ��ȭ�ϴ� �Լ�
    /// </summary>
    protected void SetVirtualCameraActive(bool isActive)
    {
        virtualCamera.SetActive(isActive);

        if (!isActive)
        {
            Camera.main.orthographicSize = 2.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.transform.root.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetInteractable(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.transform.root.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ClearInteractable();
            }
        }
    }
}

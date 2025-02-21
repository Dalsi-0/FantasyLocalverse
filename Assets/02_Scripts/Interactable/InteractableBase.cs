using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public Action onInteract; // 실행할 동작을 저장하는 델리게이트
    [SerializeField] protected GameObject virtualCamera;

    /// <summary>
    /// 상호작용을 수행하는 함수
    /// </summary>
    public void Interact()
    {
        onInteract?.Invoke();
    }

    /// <summary>
    /// 가상 카메라를 활성화 비활성화하는 함수
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

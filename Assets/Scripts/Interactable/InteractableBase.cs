using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public Action onInteract; // 실행할 동작을 저장하는 델리게이트
    [SerializeField] protected GameObject virtualCamera;

    public void Interact()
    {
        onInteract?.Invoke(); // 할당된 기능 실행
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float maxRadius;


    private void FixedUpdate()
    {
        Look();
    }

    /// <summary>
    /// 카메라 피벗을 기준으로하는 카메라 기본 움직임
    /// </summary>
    private void Look()
    {
        if (cameraPivot == null) return;

        ClampPivotToPlayer(Input.mousePosition);
        transform.position = Vector3.Lerp(transform.position, cameraPivot.position, Time.deltaTime * cameraSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    /// <summary>
    /// 카메라 피벗 위치 설정
    /// </summary>
    /// <param name="mousePosition">마우스 위치 값</param>
    private void ClampPivotToPlayer(Vector2 mousePosition)
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPos.z = 0f;

        float distance = Vector3.Distance(targetPos, GameManager.Instance.PlayerController.transform.position);
        if (distance > maxRadius)
        {
            targetPos = GameManager.Instance.PlayerController.transform.position + (targetPos - GameManager.Instance.PlayerController.transform.position).normalized * maxRadius;
        }

        cameraPivot.position = targetPos;
    }
}

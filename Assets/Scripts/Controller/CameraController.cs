using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float maxRadius;

    private GameManager gameManager;

    private void Start()
    {
        InitSetting();
    }

    private void FixedUpdate()
    {
        Look();
    }

    void InitSetting()
    {
        gameManager = GameManager.Instance;
    }

    /// <summary>
    /// ī�޶� �ǹ��� ���������ϴ� ī�޶� �⺻ ������
    /// </summary>
    void Look()
    {
        if (cameraPivot == null) return;

        ClampPivotToPlayer(Input.mousePosition);
        transform.position = Vector3.Lerp(transform.position, cameraPivot.position, Time.deltaTime * cameraSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    /// <summary>
    /// ī�޶� �ǹ� ��ġ ����
    /// </summary>
    /// <param name="mousePosition">���콺 ��ġ ��</param>
    void ClampPivotToPlayer(Vector2 mousePosition)
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPos.z = 0f;

        float distance = Vector3.Distance(targetPos, gameManager.player.transform.position);
        if (distance > maxRadius)
        {
            targetPos = gameManager.player.transform.position + (targetPos - gameManager.player.transform.position).normalized * maxRadius;
        }

        cameraPivot.position = targetPos;
    }
}

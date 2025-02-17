using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Vector2 moveDir = Vector2.zero;
    [SerializeField] float speed = 10f;

    void Start()
    {
        InitSetting();
    }



    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = pos.y * 0.01f; // Y���� �������� �� �ڿ� ��ġ
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        MoveMent();
    }

    void InitSetting()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void MoveMent()
    {

        myRigidbody2D.velocity = moveDir * speed;


    }

    #region InputSystem
    void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>().normalized;
    }
    void OnInteraction(InputValue inputValue)
    {
        Debug.Log("fff");
    }
    void OnSkill(InputValue inputValue)
    {
        // �Է� �� �������� (float: 1�̸� ���� ����)
        float input = inputValue.Get<float>();

        // Ű�� ���ȴٸ� � Ű�� ���ȴ��� üũ
        if (input > 0)
        {
            Key pressedKey = GetPressedKey();

            switch (pressedKey)
            {
                case Key.Space:
                    Debug.Log("Confirm Space");
                    break;

                case Key.R:
                    Debug.Log("Confirm R");
                    break;

                default:
                    Debug.Log("Unhandled Key");
                    break;
            }
        }
    } 
    
    // ���� ���� Ű�� ��ȯ�ϴ� �Լ�
    private Key GetPressedKey()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) return Key.Space;
        if (Keyboard.current.rKey.wasPressedThisFrame) return Key.R;

        return Key.None; // �Էµ� Ű�� ���� ���
    }

    #endregion
}

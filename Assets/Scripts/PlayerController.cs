using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Vector2 moveDir = Vector2.zero;
    [SerializeField] float speed = 10f;

    private bool isDashing = false; // ��� ���� ����

    void Start()
    {
        InitSetting();
    }

    private void FixedUpdate()
    {
        MoveMent();
    }


    void InitSetting()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();

        SkillManager.Instance.AddSkill(Key.Space, "Dash");
        SkillManager.Instance.AddSkill(Key.R, "Ride");
    }

    void MoveMent()
    {
        if (!isDashing) // ��� ���� �ƴ� ���� �̵� ó��
        {
            myRigidbody2D.velocity = moveDir * speed;
        }
    }
    public IEnumerator Dash(Vector2 dashDir, float dashSpeed, float dashTime)
    {
        isDashing = true;
        myRigidbody2D.velocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashTime); // ��� ���� �ð�

        isDashing = false;
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

            SkillManager.Instance.UseSkill(pressedKey);
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

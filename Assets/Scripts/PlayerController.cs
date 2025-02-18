using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Vector2 moveDir = Vector2.zero;
    private float lastDirection = 0f; // ������ �̵� ���� ����
    [SerializeField] float speed = 10f;

    private bool isDashing = false; // ��� ���� ����
    private bool moveLock = false; // ������ �Ұ� ����
    private bool isMoving = false;
    private bool prevIsMoving = false; // ���� �̵� ���� ����

    [SerializeField] Animator myAnimator;
    private InteractableBase currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ ����

    void Start()
    {
        InitSetting();
    }

    private void FixedUpdate()
    {
        isMoving = myRigidbody2D.velocity.sqrMagnitude > 0.01f;
        if (isMoving != prevIsMoving)
        {
            myAnimator.SetBool("1_Move", isMoving);
            prevIsMoving = isMoving; // ���� ���� ������Ʈ
        }
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
        if (moveLock) return; // �������� ��� ���¶�� �ƹ��͵� �� ��
        if (isDashing) return; // ��� �߿��� �̵� ���� ���� X
        
        myRigidbody2D.velocity = moveDir * speed;

        // �̵� ������ ����� ���� lastDirection ������Ʈ
        if (moveDir.x != 0)
        {
            lastDirection = moveDir.x;
        }

        transform.rotation = Quaternion.Euler(0f, lastDirection > 0 ? 180f : 0f, 0f);
    }
    public IEnumerator Dash(Vector2 dashDir, float dashSpeed, float dashTime)
    {
        isDashing = true;
        myRigidbody2D.velocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashTime); // ��� ���� �ð�

        isDashing = false;
    }


    public void SetInteractable(InteractableBase interactable)
    {
        currentInteractable = interactable;
    }
    public void ClearInteractable()
    {
        currentInteractable = null;
    }
    public void SetMoveLock(bool state)
    {
        moveLock = state;
        if (state)
        {
            myRigidbody2D.velocity = Vector2.zero;
        }
    }

    #region InputSystem
    void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>().normalized;
    }
    void OnInteraction(InputValue inputValue)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            ClearInteractable();
        }
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

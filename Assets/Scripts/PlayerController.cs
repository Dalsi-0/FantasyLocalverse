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

    private bool isDashing = false; // 대시 상태 변수
    private bool moveLock = false; // 움직임 불가 상태

    private InteractableBase currentInteractable; // 현재 상호작용 가능한 오브젝트 저장

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
        if (moveLock) return; // 움직임이 잠긴 상태라면 아무것도 안 함

        if (isDashing) return; // 대시 중에는 이동 방향 변경 X
        

        myRigidbody2D.velocity = moveDir * speed;
    }
    public IEnumerator Dash(Vector2 dashDir, float dashSpeed, float dashTime)
    {
        isDashing = true;
        myRigidbody2D.velocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashTime); // 대시 지속 시간

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
        // 입력 값 가져오기 (float: 1이면 눌린 상태)
        float input = inputValue.Get<float>();

        // 키가 눌렸다면 어떤 키가 눌렸는지 체크
        if (input > 0)
        {
            Key pressedKey = GetPressedKey();

            SkillManager.Instance.UseSkill(pressedKey);
        }
    } 
    
    // 현재 눌린 키를 반환하는 함수
    private Key GetPressedKey()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) return Key.Space;
        if (Keyboard.current.rKey.wasPressedThisFrame) return Key.R;

        return Key.None; // 입력된 키가 없을 경우
    }

    #endregion
}

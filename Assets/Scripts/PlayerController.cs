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
        if (!isDashing) // 대시 중이 아닐 때만 이동 처리
        {
            myRigidbody2D.velocity = moveDir * speed;
        }
    }
    public IEnumerator Dash(Vector2 dashDir, float dashSpeed, float dashTime)
    {
        isDashing = true;
        myRigidbody2D.velocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashTime); // 대시 지속 시간

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

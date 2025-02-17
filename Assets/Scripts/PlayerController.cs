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
        pos.z = pos.y * 0.01f; // Y값이 높을수록 더 뒤에 배치
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
        // 입력 값 가져오기 (float: 1이면 눌린 상태)
        float input = inputValue.Get<float>();

        // 키가 눌렸다면 어떤 키가 눌렸는지 체크
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
    
    // 현재 눌린 키를 반환하는 함수
    private Key GetPressedKey()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) return Key.Space;
        if (Keyboard.current.rKey.wasPressedThisFrame) return Key.R;

        return Key.None; // 입력된 키가 없을 경우
    }

    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStat
{
    public float speed;
    public Animator myAnimator;
    public ParticleSystem moveParticle;

    public PlayerStat(float speed, Animator myAnimator, ParticleSystem moveParticle)
    {
        this.speed = speed;
        this.myAnimator = myAnimator;
        this.moveParticle = moveParticle;
    }
}

// 탑승 미탑승
public enum EPlayerState
{
    Normal,
    Ride,
}

public class PlayerController : MonoBehaviour
{
    EPlayerState playerState = EPlayerState.Normal;

    private static PlayerStat NormalStat; // 일반 상태 능력치
    private static PlayerStat RideStat; // 탑승 상태 능력치

    [SerializeField] Transform normalTransform;
    [SerializeField] Transform rideTransform;
    [SerializeField] ParticleSystem walkMoveParticle;
    [SerializeField] ParticleSystem rideMoveParticle;
    private Coroutine moveParticleCoroutine;
    private Rigidbody2D myRigidbody2D;
    private PlayerStat usingStat; // 현재 적용 중인 능력치
    private Vector2 moveDir = Vector2.zero;
    private float lastDirection = 0f; // 마지막 이동 방향 저장
    private bool isDashing = false; // 대시 상태 변수
    private bool moveLock = false; // 움직임 불가 상태
    private bool isMoving = false; // 현재 움직임 판단
    private bool prevIsMoving = false; // 이전 이동 상태 저장
    private InteractableBase currentInteractable; // 현재 상호작용 가능한 오브젝트 저장
    private float speedFactor;

    // 옷 변경 기능 관련
    public SpriteRenderer[] upperWearNomal;
    public SpriteRenderer[] lowerWearNomal;
    public SpriteRenderer[] upperWearRide;

    private void Start()
    {
        InitSetting();
        ChangeSystemState(EPlayerState.Normal);
    }

    private void FixedUpdate()
    {
        isMoving = myRigidbody2D.velocity.sqrMagnitude > 0.01f;

        if (isMoving != prevIsMoving)
        {
            usingStat.myAnimator.SetBool("1_Move", isMoving);

            if (isMoving)
            {
                if (moveParticleCoroutine == null)
                    moveParticleCoroutine = StartCoroutine(PlayMoveParticle());
            }
            else
            {
                if (moveParticleCoroutine != null)
                {
                    StopCoroutine(moveParticleCoroutine);
                    moveParticleCoroutine = null;
                }
                walkMoveParticle.Stop();
            }

            prevIsMoving = isMoving;
        }

        speedFactor = Mathf.Clamp(myRigidbody2D.velocity.magnitude * 0.3f, 0.5f, 1.5f);
        usingStat.myAnimator.speed = isMoving ? speedFactor : 1.0f;

        MoveMent();
    }

    private void InitSetting()
    {
        SetPlayerStat();

        if (SceneManager.GetActiveScene().name != ESceneType.Village.ToString())
        {
            ShadowCaster2D shadowCaster2D = GetComponent<ShadowCaster2D>();
            shadowCaster2D.enabled = false;
        }
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// 이동 시 파티클 효과를 재생하는 코루틴
    /// </summary>
    private IEnumerator PlayMoveParticle()
    {
        while (isMoving)
        {
            if (playerState == EPlayerState.Normal)
            {
                walkMoveParticle.Play();
            }
            else
            {
                rideMoveParticle.Play();
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    /// <summary>
    /// 플레이어 상태를 변경하는 함수
    /// </summary>
    public void ChangeSystemState(EPlayerState state)
    {
        playerState = state;
        switch (playerState)
        {
            case EPlayerState.Normal:
                usingStat = NormalStat;
                normalTransform.gameObject.SetActive(true);
                rideTransform.gameObject.SetActive(false);
                break;

            case EPlayerState.Ride:
                usingStat = RideStat;
                normalTransform.gameObject.SetActive(false);
                rideTransform.gameObject.SetActive(true);
                break;
        }

        if (isMoving)
        {
            usingStat.myAnimator.SetBool("1_Move", isMoving);
        }
    }

    public EPlayerState GetPlayerState()
    {
        return playerState;
    }

    /// <summary>
    /// 플레이어 상태별 능력치를 설정하는 함수
    /// </summary>
    private void SetPlayerStat()
    {
        Animator normalAnimator = normalTransform.GetChild(0).GetComponent<Animator>();
        Animator rideAnimator = rideTransform.GetChild(0).GetComponent<Animator>();

        ParticleSystem normalParticle = normalTransform.GetChild(1).GetComponent<ParticleSystem>();
        ParticleSystem rideParticle = rideTransform.GetChild(1).GetComponent<ParticleSystem>();

        NormalStat = new PlayerStat(4f, normalAnimator, normalParticle);
        RideStat = new PlayerStat(6f, rideAnimator, rideParticle);
    }

    /// <summary>
    /// 플레이어의 이동을 처리하는 함수
    /// </summary>
    private void MoveMent()
    {
        if (moveLock) return; // 움직임이 잠긴 상태라면 아무것도 안 함
        if (isDashing) return; // 대시 중에는 이동 방향 변경 X

        myRigidbody2D.velocity = moveDir * usingStat.speed;

        // 이동 방향이 변경될 때만 lastDirection 업데이트
        if (moveDir.x != 0)
        {
            lastDirection = moveDir.x;
        }

        transform.rotation = Quaternion.Euler(0f, lastDirection > 0 ? 180f : 0f, 0f);
    }

    /// <summary>
    /// 대시를 수행하는 코루틴
    /// </summary>
    public IEnumerator Dash(Vector2 dashDir, float dashSpeed, float dashTime)
    {
        isDashing = true;
        myRigidbody2D.velocity = dashDir * dashSpeed;

        transform.rotation = Quaternion.Euler(0f, myRigidbody2D.velocity.x > 0 ? 180f : 0f, 0f);
        lastDirection = myRigidbody2D.velocity.x > 0 ? 1 : -1;

        yield return new WaitForSeconds(dashTime); // 대시 지속 시간

        isDashing = false;
    }

    /// <summary>
    /// 현재 상호작용 가능한 오브젝트를 설정하는 함수
    /// </summary>
    public void SetInteractable(InteractableBase interactable)
    {
        currentInteractable = interactable;
    }

    public void ClearInteractable()
    {
        currentInteractable = null;
    }
    /// <summary>
    /// 이동 잠금 상태를 설정하는 함수
    /// </summary>
    public void SetMoveLock(bool state)
    {
        moveLock = state;
        if (state)
        {
            myRigidbody2D.velocity = Vector2.zero;
        }
    }

    #region InputSystem
    private void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>().normalized;
    }
    private void OnInteraction(InputValue inputValue)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            ClearInteractable();
        }
    }
    private void OnSkill(InputValue inputValue)
    {
        // 입력 값 가져오기 (float: 1이면 눌린 상태)
        float input = inputValue.Get<float>();

        // 키가 눌렸다면 어떤 키가 눌렸는지 체크
        if (input > 0)
        {
            Key pressedKey = GetPressedKey();

            SkillManager.Instance.skillController.UseSkill(pressedKey);
        }
    }
    private Key GetPressedKey()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) return Key.Space;
        if (Keyboard.current.rKey.wasPressedThisFrame) return Key.R;

        return Key.None;
    }
    #endregion
}

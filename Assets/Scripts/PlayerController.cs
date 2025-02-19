using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerStat
{
    public float speed;
    public Animator myAnimator;
    public ParticleSystem dashParticle;

    public PlayerStat(float speed, Animator myAnimator, ParticleSystem dashParticle)
    {
        this.speed = speed;
        this.myAnimator = myAnimator;
        this.dashParticle = dashParticle;
    }
}

public enum EPlayerState
{
    Normal,
    Ride,
}

public class PlayerController : MonoBehaviour
{
    EPlayerState playerState = EPlayerState.Normal;

    private static PlayerStat NormalStat;
    private static PlayerStat RideStat;

    [SerializeField] Transform normalTransform;
    [SerializeField] Transform rideTransform;
    private Animator normalAnimator;
    private ParticleSystem normalDashParticle;
    private Animator rideAnimator;
    private ParticleSystem rideDashParticle;

    private Rigidbody2D myRigidbody2D;
    private PlayerStat usingStat;
    private Vector2 moveDir = Vector2.zero;
    private float lastDirection = 0f; // ������ �̵� ���� ����
    private bool isDashing = false; // ��� ���� ����
    private bool moveLock = false; // ������ �Ұ� ����
    private bool isMoving = false;
    private bool prevIsMoving = false; // ���� �̵� ���� ����
    private InteractableBase currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ ����

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
            prevIsMoving = isMoving; // ���� ���� ������Ʈ
        }

        float speedFactor = Mathf.Clamp(myRigidbody2D.velocity.magnitude * 0.3f, 0.5f, 1.5f);
        usingStat.myAnimator.speed = isMoving ? speedFactor : 1.0f;

        MoveMent();
    }

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


    private void InitSetting()
    {
        SetPlayerStat();

        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void SetPlayerStat()
    {
        Animator normalAnimator = normalTransform.GetChild(0).GetComponent<Animator>();
        Animator rideAnimator = rideTransform.GetChild(0).GetComponent<Animator>();

        ParticleSystem normalParticle = normalTransform.GetChild(1).GetComponent<ParticleSystem>();
        ParticleSystem rideParticle = rideTransform.GetChild(1).GetComponent<ParticleSystem>();

        NormalStat = new PlayerStat(4f, normalAnimator, normalParticle);
        RideStat = new PlayerStat(6f, rideAnimator, rideParticle);
    }

    private void MoveMent()
    {
        if (moveLock) return; // �������� ��� ���¶�� �ƹ��͵� �� ��
        if (isDashing) return; // ��� �߿��� �̵� ���� ���� X
        
        myRigidbody2D.velocity = moveDir * usingStat.speed;

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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public Transform hudUISkill;

    [SerializeField] Animator latterBoxAnimator;
    [SerializeField] Animator FadeAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        latterBoxAnimator.enabled = false;
        FadeAnimation();
    }



    public void ActiveOrDisableLetterbox(bool isActive)
    {
        latterBoxAnimator.enabled = true;
        if (isActive)
        {
            latterBoxAnimator.SetBool("isActive", true);

            return;
        }
        latterBoxAnimator.SetBool("isActive", false);
    }

    public void FadeAnimation()
    {
        FadeAnimator.enabled = true;
        FadeAnimator.Play("FadeIn");
    }




}

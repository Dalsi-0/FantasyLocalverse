using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public Transform hudUISkill;

    [SerializeField] Transform latterBox;
    [SerializeField] Animator latterBoxAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayLetterboxAnimation()
    {
        latterBox.gameObject.SetActive(true);
        latterBoxAnimator.Play("ShowLatterBox");
    }

    public void DisableLatterBox()
    {
        latterBox.gameObject.SetActive(false);
    }
}

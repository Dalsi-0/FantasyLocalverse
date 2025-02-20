using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }


    [SerializeField] private SkillRepository repository;
    public SkillController skillController;

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
        SetSkillPreset();
    }

    /// <summary>
    /// 현재 씬에 따라 스킬 프리셋을 설정하는 함수
    /// </summary>
    private void SetSkillPreset()
    {
        if (SceneManager.GetActiveScene().name == ESceneType.Village.ToString())
        {
            AddSkill(Key.Space, "Dash");
            AddSkill(Key.R, "Ride");
        }
        else if (SceneManager.GetActiveScene().name == ESceneType.MiniGameFind.ToString())
        {
            AddSkill(Key.Space, "Dash");
            AddSkill(Key.R, "Scan");
        }
    }

    public GameObject CreateSkillEffectPrefabs(GameObject obj, Transform parent )
    {
        return Instantiate(obj, parent);
    }

    /// <summary>
    /// 지정된 키에 스킬을 추가하는 함수
    /// </summary>
    public void AddSkill(Key key, string skillName)
    {
        // UI 추가
        GameObject obj = Instantiate(repository.GetSkillUIIcon(skillName), UIManager.Instance.hudUISkill);
        Image cooldownImage = obj.transform.GetComponent<SkillIcon>().coolDownImage;

        // 스킬 생성 및 등록
        SkillDataSO data = repository.GetSkillData(skillName);
        SkillBase skill = repository.GetSkillBase(skillName, data, cooldownImage);

        // SkillController에 등록
        skillController.AssignSkill(key, skill);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillManager : BaseManager
{
    public static SkillManager Instance { get; private set; }

    private SkillRepository skillRepository;
    private SkillController skillController;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            skillController = GetComponent<SkillController>();
            base.Awake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindRepository();
        SetSkillPreset();
    }

    /// <summary>
    /// 현재 씬에서 SkillRepository 찾기
    /// </summary>
    protected override void FindRepository()
    {
        skillRepository = FindObjectOfType<SkillRepository>();
    }

    private void SetSkillPreset()
    {
        if (skillController == null) return;

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == ESceneType.Village.ToString())
        {
            AddSkill(Key.Space, "Dash");
            AddSkill(Key.R, "Ride");
        }
        else if (sceneName == ESceneType.MiniGameFind.ToString())
        {
            AddSkill(Key.Space, "Dash");
            AddSkill(Key.R, "Scan");
        }
    }

    public void UseSkill(Key key)
    {
        if (skillController != null)
        {
            skillController.UseSkill(key);
        }
    }

    private void AddSkill(Key key, string skillName)
    {
        GameObject obj = Instantiate(skillRepository.GetSkillUIIcon(skillName), UIManager.Instance.hudUISkill);
        Image cooldownImage = obj.GetComponent<SkillIcon>().coolDownImage;

        SkillDataSO data = skillRepository.GetSkillData(skillName);
        SkillBase skill = skillRepository.GetSkillBase(skillName, data, cooldownImage);

        skillController.AssignSkill(key, skill);
    }
    
    /// <summary>
     /// 스킬 이펙트 프리팹 생성 함수
     /// </summary>
    public GameObject CreateSkillEffectPrefabs(GameObject effectPrefab, Transform parent)
    {
        if (effectPrefab == null)
        {
            return null;
        }

        return Instantiate(effectPrefab, parent);
    }

    public void ResetSkill()
    {
        skillController.ResetSkill();
    }
}

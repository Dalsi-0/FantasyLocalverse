using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    private Dictionary<Key, SkillBase> skillBindings = new Dictionary<Key, SkillBase>();

    public SkillRepository repository;

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

    private void Update()
    {
        UpdateSkills();
    }
    
    void UpdateSkills()
    {
        float deltaTime = Time.deltaTime;
        foreach (SkillBase skill in skillBindings.Values)
        {
            skill.UpdateCooldown(deltaTime);
        }
    }

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


    public void AddSkill(Key key, string skillName)
    {
        // UI 추가
        GameObject obj = Instantiate(repository.GetSkillUIIcon(skillName), UIManager.Instance.hudUISkill);
        Image cooldownImage = obj.transform.GetComponent<SkillIcon>().coolDownImage;

        // 스킬 생성 및 등록
        SkillDataSO Data = repository.GetSkillData(skillName);
        skillBindings[key] = repository.GetSkillBase(skillName, Data, cooldownImage);  // 키에 해당하는 스킬 등록
    }

    public void UseSkill(Key keyCode)
    {
        if (skillBindings.ContainsKey(keyCode) && skillBindings[keyCode] != null)
        {
            skillBindings[keyCode].UseSkill();
        }
        else
        {
            Debug.Log($"{keyCode}에 할당된 스킬이 없습니다.");
        }
    }
}

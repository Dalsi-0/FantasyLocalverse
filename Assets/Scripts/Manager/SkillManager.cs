using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    private Dictionary<Key, SkillBase> skillBindings = new Dictionary<Key, SkillBase>();

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


    public void AddSkill(Key key, string skillName)
    {
        // UI �߰�
        GameObject obj = Instantiate(SkillRepository.Instance.GetSkillUIIcon(skillName), UIManager.Instance.hudUISkill);
        Image cooldownImage = obj.transform.GetComponent<SkillIcon>().coolDownImage;

        // ��ų ���� �� ���
        SkillData Data = SkillRepository.Instance.GetSkillData(skillName);
        skillBindings[key] = SkillRepository.Instance.GetSkillBase(skillName, Data, cooldownImage);  // Ű�� �ش��ϴ� ��ų ���
    }

    public void UseSkill(Key keyCode)
    {
        if (skillBindings.ContainsKey(keyCode) && skillBindings[keyCode] != null)
        {
            skillBindings[keyCode].UseSkill();
        }
        else
        {
            Debug.Log($"{keyCode}�� �Ҵ�� ��ų�� �����ϴ�.");
        }
    }
}

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
        GameObject obj = Instantiate(repository.GetSkillUIIcon(skillName), UIManager.Instance.hudUISkill);
        Image cooldownImage = obj.transform.GetComponent<SkillIcon>().coolDownImage;

        // ��ų ���� �� ���
        SkillData Data = repository.GetSkillData(skillName);
        skillBindings[key] = repository.GetSkillBase(skillName, Data, cooldownImage);  // Ű�� �ش��ϴ� ��ų ���
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

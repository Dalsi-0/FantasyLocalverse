using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillController : MonoBehaviour
{
    private Dictionary<Key, SkillBase> skillBindings = new Dictionary<Key, SkillBase>();

    private void Update()
    {
        UpdateSkillsCooldown();
    }

    /// <summary>
    /// ��� ��ų�� ��Ÿ���� ������Ʈ�ϴ� �Լ�
    /// </summary>
    private void UpdateSkillsCooldown()
    {
        float deltaTime = Time.deltaTime;
        foreach (SkillBase skill in skillBindings.Values)
        {
            skill.UpdateCooldown(deltaTime);
        }
    }

    /// <summary>
    /// Ư�� Ű�� ��ų�� �Ҵ��ϴ� �Լ�
    /// </summary>
    public void AssignSkill(Key key, SkillBase skill)
    {
        skillBindings[key] = skill;
    }

    /// <summary>
    /// Ư�� Ű�� �Ҵ�� ��ų�� ����ϵ��� �ϴ� �Լ�
    /// </summary>
    public void UseSkill(Key key)
    {
        if (skillBindings.ContainsKey(key) && skillBindings[key] != null)
        {
            skillBindings[key].UseSkill();
        }
        else
        {
            Debug.Log($"{key}�� �Ҵ�� ��ų�� �����ϴ�.");
        }
    }

    public void ResetSkill()
    {
        skillBindings.Clear();
    }
}
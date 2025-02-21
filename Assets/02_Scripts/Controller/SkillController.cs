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
    /// 모든 스킬의 쿨타임을 업데이트하는 함수
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
    /// 특정 키에 스킬을 할당하는 함수
    /// </summary>
    public void AssignSkill(Key key, SkillBase skill)
    {
        skillBindings[key] = skill;
    }

    /// <summary>
    /// 특정 키에 할당된 스킬을 사용하도록 하는 함수
    /// </summary>
    public void UseSkill(Key key)
    {
        if (skillBindings.ContainsKey(key) && skillBindings[key] != null)
        {
            skillBindings[key].UseSkill();
        }
        else
        {
            Debug.Log($"{key}에 할당된 스킬이 없습니다.");
        }
    }

    public void ResetSkill()
    {
        skillBindings.Clear();
    }
}